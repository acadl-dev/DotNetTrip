using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetTrip.Data;
using DotNetTrip.Models;

namespace DotNetTrip.Pages.Destino
{
    public class CreateModel : PageModel
    {
        private readonly DotNetTrip.Data.ApplicationDbContext _context;

        public CreateModel(DotNetTrip.Data.ApplicationDbContext context)
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

            return RedirectToPage("./Index");
        }
    }
}
