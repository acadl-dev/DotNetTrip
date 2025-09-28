using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DotNetTrip.Models;

namespace DotNetTrip.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; } = default!;
        public DbSet<Destino> Destino { get; set; } = default!;
        public DbSet<PacoteTuristico> PacoteTuristico { get; set; } = default!;
        public DbSet<Reserva> Reserva { get; set; } = default!;
    }
}
