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
    public class DetailsModel : PageModel
    {
        private readonly DotNetTrip.Data.ApplicationDbContext _context;

        public DetailsModel(DotNetTrip.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
