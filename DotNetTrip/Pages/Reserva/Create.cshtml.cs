using DotNetTrip.Data;
using DotNetTrip.Models;
using DotNetTrip.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetTrip.Pages.Reserva
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
            ViewData["ClienteId"] = new SelectList(
            _context.Clientes.Select(c => new {
                Id = c.Id,
                Display = $"{c.Id} - {c.Nome}"
            }),
            "Id",
            "Display"
        );

            ViewData["PacoteTuristicoId"] = new SelectList(
                _context.PacoteTuristico.Select(p => new {
                    Id = p.Id,
                    Display = $"{p.Id} - {p.Titulo}"
                }),
                "Id",
                "Display"
            );

            return Page();
        }

        [BindProperty]
        public Models.Reserva Reserva { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var erro in erros)
                {
                    Console.WriteLine(erro.ErrorMessage);
                }
                return Page();
            }

            _context.Reserva.Add(Reserva);
            await _context.SaveChangesAsync();


            // Montando a mensagem com dados reais da reserva
            string mensagem = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - Novo pacote criado com sucesso: Id={Reserva.Id}, ClienteId={Reserva.ClienteId}, PacoteTuristicoId={Reserva.PacoteTuristicoId}";
            WatchDog.LogActions?.Invoke(mensagem);

            return RedirectToPage("./Index");
        }
    }
}
