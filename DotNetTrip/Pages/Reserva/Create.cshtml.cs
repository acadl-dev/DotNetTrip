using DotNetTrip.Data;
using DotNetTrip.Models;
using DotNetTrip.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace DotNetTrip.Pages.Reserva
{
    public class CreateModel : PageModel
    {
        private readonly DotNetTripDbContext _context;

        public CreateModel(DotNetTripDbContext context)
        {
            _context = context;

            // Registrar o delegate para o evento de capacidade atingida
            Models.Reserva.CapacityReached += LogCapacityReached;
        }

        public IActionResult OnGet()
        {
            // ===== FILTRAR APENAS PACOTES COM DATA FUTURA E VAGAS DISPONÍVEIS =====
            var pacotesDisponiveis = _context.PacoteTuristico
                .Include(p => p.ReservasFeitas)
                .AsEnumerable() // Trazer para memória para fazer cálculos
                .Where(p => p.DataInicio > DateTime.Now && // Data futura
                           p.ReservasFeitas.Count < p.CapacidadeMaxima) // Tem vagas
                .Select(p => new {
                    Id = p.Id,
                    Display = $"{p.Id} - {p.Titulo} (Vagas: {p.CapacidadeMaxima - p.ReservasFeitas.Count}/{p.CapacidadeMaxima}) - {p.DataInicio:dd/MM/yyyy}"
                })
                .ToList();

            ViewData["ClienteId"] = new SelectList(
                _context.Clientes.Select(c => new {
                    Id = c.Id,
                    Display = $"{c.Id} - {c.Nome}"
                }),
                "Id",
                "Display"
            );

            ViewData["PacoteTuristicoId"] = new SelectList(
                pacotesDisponiveis,
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

            // Verificar se o pacote existe e carregar suas informações
            var pacote = await _context.PacoteTuristico
                .Include(p => p.ReservasFeitas)
                .FirstOrDefaultAsync(p => p.Id == Reserva.PacoteTuristicoId);

            if (pacote == null)
            {
                ModelState.AddModelError("", "Pacote turístico não encontrado.");
                await RecarregarViewData();
                return Page();
            }

            // ===== NOVA VALIDAÇÃO 1: Verificar se a data do pacote é futura =====
            if (pacote.DataInicio <= DateTime.Now)
            {
                string mensagemLog = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - ALERTA: Tentativa de reserva bloqueada. Pacote '{pacote.Titulo}' (ID: {pacote.Id}) tem data de início no passado ou hoje ({pacote.DataInicio:dd/MM/yyyy}).";
                Console.WriteLine(mensagemLog);
                WatchDog.LogActions?.Invoke(mensagemLog);

                ModelState.AddModelError("", $"Não é possível criar a reserva. O pacote '{pacote.Titulo}' tem data de início em {pacote.DataInicio:dd/MM/yyyy}. Apenas pacotes com data futura podem ser reservados.");
                await RecarregarViewData();
                return Page();
            }

            // ===== NOVA VALIDAÇÃO 2: Verificar se há vagas disponíveis =====
            int reservasAtuais = pacote.ReservasFeitas.Count;

            if (reservasAtuais >= pacote.CapacidadeMaxima)
            {
                // Disparar o evento de capacidade atingida
                string mensagemEvento = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - ALERTA: Tentativa de reserva para pacote '{pacote.Titulo}' (ID: {pacote.Id}) rejeitada. Capacidade máxima de {pacote.CapacidadeMaxima} já atingida. Reservas atuais: {reservasAtuais}";
                Models.Reserva.OnCapacityReached(mensagemEvento);

                ModelState.AddModelError("", $"Não é possível criar a reserva. O pacote '{pacote.Titulo}' já atingiu sua capacidade máxima de {pacote.CapacidadeMaxima} reservas. Não há vagas disponíveis.");
                await RecarregarViewData();
                return Page();
            }

            // ===== VALIDAÇÃO 3: Verificar se o cliente já tem reserva para este pacote =====
            var reservaExistente = await _context.Reserva
                .FirstOrDefaultAsync(r => r.ClienteId == Reserva.ClienteId
                                       && r.PacoteTuristicoId == Reserva.PacoteTuristicoId);

            if (reservaExistente != null)
            {
                // Buscar informações do cliente para mensagem mais clara
                var cliente = await _context.Clientes.FindAsync(Reserva.ClienteId);

                string mensagemLog = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - ALERTA: Tentativa de reserva duplicada bloqueada. Cliente '{cliente?.Nome}' (ID: {Reserva.ClienteId}) já possui reserva (ID: {reservaExistente.Id}) para o pacote '{pacote.Titulo}' (ID: {Reserva.PacoteTuristicoId}).";
                Console.WriteLine(mensagemLog);
                WatchDog.LogActions?.Invoke(mensagemLog);

                ModelState.AddModelError("", $"O cliente '{cliente?.Nome}' já possui uma reserva para o pacote '{pacote.Titulo}'. Não é permitido reservar o mesmo pacote mais de uma vez.");
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
            // ===== FILTRAR APENAS PACOTES COM DATA FUTURA E VAGAS DISPONÍVEIS =====
            var pacotesDisponiveis = _context.PacoteTuristico
                .Include(p => p.ReservasFeitas)
                .AsEnumerable()
                .Where(p => p.DataInicio > DateTime.Now &&
                           p.ReservasFeitas.Count < p.CapacidadeMaxima)
                .Select(p => new {
                    Id = p.Id,
                    Display = $"{p.Id} - {p.Titulo} (Vagas: {p.CapacidadeMaxima - p.ReservasFeitas.Count}/{p.CapacidadeMaxima}) - {p.DataInicio:dd/MM/yyyy}"
                })
                .ToList();

            ViewData["ClienteId"] = new SelectList(
                _context.Clientes.Select(c => new {
                    Id = c.Id,
                    Display = $"{c.Id} - {c.Nome}"
                }),
                "Id",
                "Display"
            );

            ViewData["PacoteTuristicoId"] = new SelectList(
                pacotesDisponiveis,
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