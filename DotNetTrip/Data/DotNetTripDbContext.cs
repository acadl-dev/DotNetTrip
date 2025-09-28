using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DotNetTrip.Data.Configurations;
using DotNetTrip.Models;


namespace DotNetTrip.Data
{
    public class DotNetTripDbContext : IdentityDbContext
    {
        public DotNetTripDbContext(DbContextOptions<DotNetTripDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new DestinoConfiguration());
            modelBuilder.ApplyConfiguration(new PacoteTuristicoConfiguration());
            modelBuilder.ApplyConfiguration(new ReservaConfiguration());

        }


        public DbSet<Cliente> Clientes { get; set; } = default!;
        public DbSet<Destino> Destino { get; set; } = default!;
        public DbSet<PacoteTuristico> PacoteTuristico { get; set; } = default!;
        public DbSet<Reserva> Reserva { get; set; } = default!;
    }
}
