using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DotNetTrip.Models;

namespace DotNetTrip.Data.Configurations
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.HasOne(r => r.Cliente)
                .WithMany()
                .HasForeignKey(r => r.ClienteId)
                .IsRequired(true);

            builder.HasOne(r => r.PacoteTuristico)
                .WithMany()
                .HasForeignKey(r => r.PacoteTuristicoId)
                .IsRequired(true);
        }
    }
}