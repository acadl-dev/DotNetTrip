using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetTrip.Data;
using DotNetTrip.Models;

namespace DotNetTrip.Pages.PacoteTuristico
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
        public Models.PacoteTuristico PacoteTuristico { get; set; } = default!;

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

            return RedirectToPage("./Index");
        }
    }
}
