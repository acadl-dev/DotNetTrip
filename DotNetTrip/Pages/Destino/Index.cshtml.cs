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
    public class IndexModel : PageModel
    {
        private readonly DotNetTrip.Data.DotNetTripDbContext _context;

        public IndexModel(DotNetTrip.Data.DotNetTripDbContext context)
        {
            _context = context;
        }

        public IList<DotNetTrip.Models.Destino> Destino { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Destino = await _context.Destino.ToListAsync();
        }
    }
}
