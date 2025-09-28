using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DotNetTrip.Data;
using DotNetTrip.Models;

namespace DotNetTrip.Pages.Reserva
{
    public class DeleteModel : PageModel
    {
        private readonly DotNetTrip.Data.DotNetTripDbContext _context;

        public DeleteModel(DotNetTrip.Data.DotNetTripDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Reserva Reserva { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FirstOrDefaultAsync(m => m.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }
            else
            {
                Reserva = reserva;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva != null)
            {
                Reserva = reserva;
                _context.Reserva.Remove(Reserva);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
