using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DotNetTrip.Data;
using DotNetTrip.Models;

namespace DotNetTrip.Pages.Cliente
{
    public class IndexModel : PageModel
    {
        private readonly DotNetTrip.Data.DotNetTripDbContext _context;

        public IndexModel(DotNetTrip.Data.DotNetTripDbContext context)
        {
            _context = context;
        }

        public IList<DotNetTrip.Models.Cliente> Cliente { get;set; } = default!;
        // Altere o nome da propriedade para evitar conflito com o namespace "Cliente"
        public IList<DotNetTrip.Models.Cliente> Clientes { get; set; } = default!;
        public async Task OnGetAsync()
        {
            Cliente = await _context.Clientes.ToListAsync();
        }
    }
}
