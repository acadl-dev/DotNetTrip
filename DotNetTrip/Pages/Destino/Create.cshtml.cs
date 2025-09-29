using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetTrip.Data;
using DotNetTrip.Models;
using DotNetTrip.Utils;

namespace DotNetTrip.Pages.Destino
{
    public class CreateModel : PageModel
    {
        private readonly DotNetTrip.Data.DotNetTripDbContext _context;

        public CreateModel(DotNetTrip.Data.DotNetTripDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DotNetTrip.Models.Destino Destino { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Destino.Add(Destino);

            await _context.SaveChangesAsync();

            // Montando a mensagem com dados reais da reserva
            string mensagem = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - Novo destino criado com sucesso: Id={Destino.Id}, Cidade={Destino.Cidade}, Pais={Destino.Pais}";
            WatchDog.LogActions?.Invoke(mensagem);

            return RedirectToPage("./Index");
        }
    }
}
