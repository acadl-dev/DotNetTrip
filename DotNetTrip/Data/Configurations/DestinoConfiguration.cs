using DotNetTrip.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetTrip.Data.Configurations
{
    public class DestinoConfiguration : IEntityTypeConfiguration<Destino>
    {
        public void Configure(EntityTypeBuilder<Destino> builder)
        {
            builder.Property(e => e.Id).HasColumnName("id_destino");
            builder.Property(e => e.Cidade).HasMaxLength(50);
            builder.Property(e => e.Pais).HasMaxLength(50);


        }

    }
}
