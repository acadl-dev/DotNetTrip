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

namespace DotNetTrip.Pages.Reserva
{
    public class CreateModel : PageModel
    {
        private readonly DotNetTrip.Data.DotNetTripDbContext _context;

        public CreateModel(DotNetTrip.Data.DotNetTripDbContext context)
        {
            _context = context;

            // Registrar o delegate para o evento de capacidade atingida
            Models.Reserva.CapacityReached += LogCapacityReached;
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var erro in erros)
                {
                    Console.WriteLine(erro.ErrorMessage);
                }

                // Recarregar os ViewData para os selects
                await RecarregarViewData();
                return Page();
            }

            // Verificar capacidade do pacote antes de criar a reserva
            var pacote = await _context.PacoteTuristico
                .Include(p => p.ReservasFeitas)
                .FirstOrDefaultAsync(p => p.Id == Reserva.PacoteTuristicoId);

            if (pacote == null)
            {
                ModelState.AddModelError("", "Pacote turístico não encontrado.");
                await RecarregarViewData();
                return Page();
            }

            // Contar reservas atuais para este pacote
            int reservasAtuais = pacote.ReservasFeitas.Count;

            // Verificar se já atingiu a capacidade máxima
            if (reservasAtuais >= pacote.CapacidadeMaxima)
            {
                // Disparar o evento de capacidade atingida
                string mensagemEvento = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - ALERTA: Tentativa de reserva para pacote '{pacote.Titulo}' (ID: {pacote.Id}) rejeitada. Capacidade máxima de {pacote.CapacidadeMaxima} já atingida. Reservas atuais: {reservasAtuais}";
                Models.Reserva.OnCapacityReached(mensagemEvento);

                ModelState.AddModelError("", $"Não é possível criar a reserva. O pacote '{pacote.Titulo}' já atingiu sua capacidade máxima de {pacote.CapacidadeMaxima} reservas.");
                await RecarregarViewData();
                return Page();
            }

            // Se chegou até aqui, pode criar a reserva
            _context.Reserva.Add(Reserva);
            await _context.SaveChangesAsync();

            Console.WriteLine($"ClienteId recebido: {Reserva.ClienteId}");
            Console.WriteLine($"PacoteTuristicoId recebido: {Reserva.PacoteTuristicoId}");

            // Verificar se após criar esta reserva a capacidade foi atingida
            if (reservasAtuais + 1 == pacote.CapacidadeMaxima)
            {
                string mensagemCapacidadeAtingida = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - AVISO: Pacote '{pacote.Titulo}' (ID: {pacote.Id}) atingiu sua capacidade máxima de {pacote.CapacidadeMaxima} reservas.";
                Models.Reserva.OnCapacityReached(mensagemCapacidadeAtingida);
            }

            // Montando a mensagem com dados reais da reserva
            string mensagem = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - Nova reserva criada com sucesso: Id={Reserva.Id}, ClienteId={Reserva.ClienteId}, PacoteTuristicoId={Reserva.PacoteTuristicoId}";
            WatchDog.LogActions?.Invoke(mensagem);

            return RedirectToPage("./Index");
        }

        private async Task RecarregarViewData()
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
        }

        // Método delegate para registrar mensagens de capacidade atingida
        private static void LogCapacityReached(string message)
        {
            Console.WriteLine("=== EVENTO DE CAPACIDADE ATINGIDA ===");
            Console.WriteLine(message);
            Console.WriteLine("=====================================");
        }

        
    }
}