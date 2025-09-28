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
    public class IndexModel : PageModel
    {
        private readonly DotNetTripDbContext _context;

        public IndexModel(DotNetTripDbContext context)
        {
            _context = context;
        }

        public IList<Models.PacoteTuristico> PacoteTuristico { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PacoteTuristico = await _context.PacoteTuristico.ToListAsync();
        }
    }
}
