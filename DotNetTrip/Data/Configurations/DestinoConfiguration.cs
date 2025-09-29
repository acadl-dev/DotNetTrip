using DotNetTrip.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetTrip.Data.Configurations
{
    public class DestinoConfiguration : IEntityTypeConfiguration<Destino>
    {
        public void Configure(EntityTypeBuilder<Destino> builder)
        {
            // Configurações existentes
            builder.Property(e => e.Id).HasColumnName("id_destino");
            builder.Property(e => e.Cidade).HasMaxLength(50);
            builder.Property(e => e.Pais).HasMaxLength(50);

            // ADICIONAR: Configurar relacionamento muitos-para-muitos com PacoteTuristico
            builder.HasMany(d => d.PacotesTuristicos)
                .WithMany(p => p.Destinos)
                .UsingEntity<Dictionary<string, object>>(
                    "PacoteTuristicoDestino", // Nome da tabela intermediária
                    j => j.HasOne<PacoteTuristico>()
                          .WithMany()
                          .HasForeignKey("PacoteTuristicoId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Destino>()
                          .WithMany()
                          .HasForeignKey("DestinoId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("DestinoId", "PacoteTuristicoId");
                        j.ToTable("PacoteTuristicoDestino");
                    });
        }
    }
}