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
    public class DetailsModel : PageModel
    {
        private readonly DotNetTrip.Data.DotNetTripDbContext _context;

        public DetailsModel(DotNetTrip.Data.DotNetTripDbContext context)
        {
            _context = context;
        }

        public Models.Reserva Reserva { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var reserva = await _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.PacoteTuristico)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            Reserva = reserva;
            return Page();
        }
    }
}
