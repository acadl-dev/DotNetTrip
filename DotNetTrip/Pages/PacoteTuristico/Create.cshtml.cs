using DotNetTrip.Data;
using DotNetTrip.Models;
using DotNetTrip.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly DotNetTripDbContext _context;

        // Delegate para aplicar desconto de 10%
        public CalculateDelegate CalcularDesconto;

        public CreateModel(DotNetTripDbContext context)
        {
            _context = context;
            // Inicializa o delegate com a lógica de desconto de 10%
            CalcularDesconto = valor => valor * 0.10m;
        }

        [BindProperty]
        public Models.PacoteTuristico PacoteTuristico { get; set; } = default!;

        // ADICIONAR: Lista de destinos disponíveis
        public List<Models.Destino> DestinosDisponiveis { get; set; } = new List<Models.Destino>();

        // ADICIONAR: IDs dos destinos selecionados
        [BindProperty]
        public List<int> DestinosSelecionados { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync()
        {
            // Carregar todos os destinos disponíveis
            DestinosDisponiveis = await _context.Destino.ToListAsync();
            return Page();
        }

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var erro in erros)
                {
                    Console.WriteLine(erro.ErrorMessage);
                }

                // Recarregar destinos se houver erro
                DestinosDisponiveis = await _context.Destino.ToListAsync();
                return Page();
            }

            // ADICIONAR: Vincular os destinos selecionados ao pacote
            if (DestinosSelecionados != null && DestinosSelecionados.Any())
            {
                var destinos = await _context.Destino
                    .Where(d => DestinosSelecionados.Contains(d.Id))
                    .ToListAsync();

                PacoteTuristico.Destinos = destinos;
            }

            _context.PacoteTuristico.Add(PacoteTuristico);
            await _context.SaveChangesAsync();

            // Montando a mensagem com dados reais do pacote
            string mensagem = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - Novo pacote criado com sucesso: Id={PacoteTuristico.Id}, Titulo={PacoteTuristico.Titulo}, Preço={PacoteTuristico.Preco}, Destinos={PacoteTuristico.Destinos.Count}";
            WatchDog.LogActions?.Invoke(mensagem);

            return RedirectToPage("./Index");
        }
    }
}