using DotNetTrip.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        builder.ToTable("Reserva");
        builder.HasKey(r => r.Id);

        // Apenas propriedades básicas, SEM relacionamentos
        builder.Property(r => r.ClienteId).IsRequired();
        builder.Property(r => r.PacoteTuristicoId).IsRequired();
        builder.Property(r => r.DataReserva).IsRequired();
    }
}