using DotNetTrip.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetTrip.Data.Configurations
{
    public class PacoteTuristicoConfiguration : IEntityTypeConfiguration<PacoteTuristico>
    {
        public void Configure(EntityTypeBuilder<PacoteTuristico> builder)
        {
            builder.Property(e => e.Id).HasColumnName("id_pacote");
            builder.Property(e => e.Titulo).HasMaxLength(50);


        }

    }
    
}
