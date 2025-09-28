using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DotNetTrip.Data;
using DotNetTrip.Models;

namespace DotNetTrip.Pages.Destino
{
    public class DeleteModel : PageModel
    {
        private readonly DotNetTrip.Data.ApplicationDbContext _context;

        public DeleteModel(DotNetTrip.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DotNetTrip.Models.Destino Destino { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destino = await _context.Destino.FirstOrDefaultAsync(m => m.Id == id);

            if (destino == null)
            {
                return NotFound();
            }
            else
            {
                Destino = destino;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destino = await _context.Destino.FindAsync(id);
            if (destino != null)
            {
                Destino = destino;
                _context.Destino.Remove(Destino);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
