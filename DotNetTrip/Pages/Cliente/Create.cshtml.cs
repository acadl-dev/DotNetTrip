using DotNetTrip.Data;
using DotNetTrip.Models;
using DotNetTrip.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DotNetTrip.Pages.Cliente
{
    public class CreateModel : PageModel
    {
        private readonly DotNetTripDbContext _context;

        public CreateModel(DotNetTripDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Inicializa o cliente com data de nascimento padrão
            Cliente = new Models.Cliente
            {
                DataNascimento = DateTime.Now.AddYears(-18) // Idade mínima de 18 anos
            };
            return Page();
        }

        [BindProperty]
        public Models.Cliente Cliente { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            // Validação customizada adicional
            ValidarIdadeMinima();
            ValidarCpfUnico();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Define a data de cadastro no momento da criação
                Cliente.DataCadastro = DateTime.Now;

                _context.Clientes.Add(Cliente);
                await _context.SaveChangesAsync();

                // Montando a mensagem de log com mais informações
                string mensagem = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - Novo cliente criado com sucesso: " +
                                $"Id={Cliente.Id}, Nome={Cliente.Nome}, Email={Cliente.Email}, CPF={Cliente.Cpf}";

                WatchDog.LogActions?.Invoke(mensagem);

                // Define mensagem de sucesso
                TempData["SuccessMessage"] = $"Cliente '{Cliente.Nome}' criado com sucesso!";

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Log do erro
                string mensagemErro = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - Erro ao criar cliente: {ex.Message}";
                WatchDog.LogActions?.Invoke(mensagemErro);

                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o cliente. Tente novamente.");
                return Page();
            }
        }

        private void ValidarIdadeMinima()
        {
            var idade = DateTime.Now.Year - Cliente.DataNascimento.Year;
            if (DateTime.Now.DayOfYear < Cliente.DataNascimento.DayOfYear)
                idade--;

            if (idade < 18)
            {
                ModelState.AddModelError("Cliente.DataNascimento", "O cliente deve ter pelo menos 18 anos.");
            }

            if (Cliente.DataNascimento > DateTime.Now)
            {
                ModelState.AddModelError("Cliente.DataNascimento", "A data de nascimento não pode ser no futuro.");
            }
        }

        private void ValidarCpfUnico()
        {
            // Verifica se já existe um cliente com o mesmo CPF
            var cpfExistente = _context.Clientes.Any(c => c.Cpf == Cliente.Cpf);
            if (cpfExistente)
            {
                ModelState.AddModelError("Cliente.Cpf", "Já existe um cliente cadastrado com este CPF.");
            }
        }
    }
}