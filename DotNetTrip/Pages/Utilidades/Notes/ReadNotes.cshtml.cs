using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;


namespace DotNetTrip.Pages.Utilidades.Anotacoes
{
    public class anotacoesModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        // Caminho do arquivo gerado
        public string? FilePath { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Diretório wwwroot/files
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Nome do arquivo com timestamp
            var fileName = $"note-{DateTime.Now:yyyyMMddHHmmss}.txt";
            var fullPath = Path.Combine(folderPath, fileName);

            // Escreve o conteúdo no arquivo
            System.IO.File.WriteAllText(fullPath, Input.Content);

            // Salva o caminho relativo para link de download
            FilePath = $"/files/{fileName}";

            // Limpa o formulário
            ModelState.Clear();
            Input.Content = string.Empty;

            return Page();
        }

        public class InputModel
        {
            [Required(ErrorMessage = "O conteúdo da nota é obrigatório")]
            [MinLength(1, ErrorMessage = "O conteúdo da nota não pode ser vazio")]
            public string Content { get; set; } = string.Empty;
        }
    }
}

