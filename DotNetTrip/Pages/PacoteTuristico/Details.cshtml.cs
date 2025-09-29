using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DotNetTrip.Data;
using DotNetTrip.Models;

namespace DotNetTrip.Pages.PacoteTuristico
{
    public class DetailsModel : PageModel
    {
        private readonly DotNetTripDbContext _context;

        public DetailsModel(DotNetTripDbContext context)
        {
            _context = context;
        }

        // Altere o nome da propriedade para evitar conflito com o namespace
        public Models.PacoteTuristico PacoteTuristico { get; set; } = default!;
        public int QuantidadeReservas { get; set; }
        public decimal ValorTotalReservas { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            // Inclui as reservas
            var pacote = await _context.PacoteTuristico
                .Include(p => p.ReservasFeitas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pacote == null)
                return NotFound();

            PacoteTuristico = pacote;

            // Func para calcular valor total
            Func<int, decimal, decimal> calcularTotal = (qtd, preco) => qtd * preco;

            QuantidadeReservas = pacote.ReservasFeitas?.Count ?? 0;
            ValorTotalReservas = calcularTotal(QuantidadeReservas, pacote.Preco);

            return Page();
        }
    }
}
