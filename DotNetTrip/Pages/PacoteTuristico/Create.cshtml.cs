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

namespace DotNetTrip.Pages.PacoteTuristico
{
    // Delegate personalizado para cálculo de descontos
    public delegate decimal CalculateDelegate(decimal valor);

    public class CreateModel : PageModel
    {
        private readonly DotNetTrip.Data.DotNetTripDbContext _context;

        // Delegate para aplicar desconto de 10%
        public CalculateDelegate CalcularDesconto;

        public CreateModel(DotNetTrip.Data.DotNetTripDbContext context)
        {
            _context = context;

            // Inicializa o delegate com a lógica de desconto de 10%
            CalcularDesconto = valor => valor * 0.10m; // Desconto de 10%
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.PacoteTuristico PacoteTuristico { get; set; } = default!;

        // Método para calcular desconto via AJAX
        public JsonResult OnPostCalcularDesconto([FromBody] decimal preco)
        {
            if (preco > 0)
            {
                var valorDesconto = CalcularDesconto(preco);
                var precoComDesconto = preco - valorDesconto;

                return new JsonResult(new
                {
                    sucesso = true,
                    precoOriginal = preco,
                    valorDesconto = valorDesconto,
                    precoComDesconto = precoComDesconto
                });
            }

            return new JsonResult(new { sucesso = false });
        }

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

            _context.PacoteTuristico.Add(PacoteTuristico);
            await _context.SaveChangesAsync();

            // Montando a mensagem com dados reais da reserva
            string mensagem = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - Novo pacote criado com sucesso: Id={PacoteTuristico.Id}, Titulo={PacoteTuristico.Titulo}, Preço={PacoteTuristico.Preco}";
            WatchDog.LogActions?.Invoke(mensagem);

            return RedirectToPage("./Index");
        }
    }
}