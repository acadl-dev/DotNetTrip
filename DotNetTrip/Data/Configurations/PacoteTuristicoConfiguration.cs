using DotNetTrip.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PacoteTuristicoConfiguration : IEntityTypeConfiguration<PacoteTuristico>
{
    public void Configure(EntityTypeBuilder<PacoteTuristico> builder)
    {
        builder.Property(e => e.Id).HasColumnName("id_pacote");
        builder.Property(e => e.Titulo).HasMaxLength(50);

        // Configure o relacionamento para a propriedade única
        builder.HasMany(p => p.ReservasFeitas)
               .WithOne(r => r.PacoteTuristico)
               .HasForeignKey(r => r.PacoteTuristicoId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}