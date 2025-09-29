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

            // ===== Adicionar filtro global para exclusão lógica =====
            // Isso faz com que clientes excluídos não apareçam em queries normais
            builder.HasQueryFilter(c => !c.IsDeleted);


        }

    }
}
