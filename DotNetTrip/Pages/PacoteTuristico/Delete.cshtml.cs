using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DotNetTrip.Data;
using DotNetTrip.Models;

namespace DotNetTrip.Pages.PacoteTuristico
{
    public class DeleteModel : PageModel
    {
        private readonly DotNetTrip.Data.DotNetTripDbContext _context;

        public DeleteModel(DotNetTrip.Data.DotNetTripDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DotNetTrip.Models.PacoteTuristico PacoteTuristico { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteturistico = await _context.PacoteTuristico.FirstOrDefaultAsync(m => m.Id == id);

            if (pacoteturistico == null)
            {
                return NotFound();
            }
            else
            {
                PacoteTuristico = pacoteturistico;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteturistico = await _context.PacoteTuristico.FindAsync(id);
            if (pacoteturistico != null)
            {
                PacoteTuristico = pacoteturistico;
                _context.PacoteTuristico.Remove(PacoteTuristico);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
