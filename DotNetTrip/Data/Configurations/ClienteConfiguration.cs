using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DotNetTrip.Models;

namespace DotNetTrip.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.Property(e => e.Id).HasColumnName("id_cliente");
            builder.Property(e => e.Nome).HasMaxLength(50);
           

        }

    }
}
