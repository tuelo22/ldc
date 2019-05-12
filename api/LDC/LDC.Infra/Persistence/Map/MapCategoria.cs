using LDC.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LDC.Infra.Persistence.Map
{
    public class MapCategoria : EntityTypeConfiguration<Categoria>
    {
        public MapCategoria()
        {
            ToTable("Categoria");

            HasKey(p => p.Id);

            Property(p => p.Nome).HasMaxLength(50).IsRequired().HasColumnName("Nome");
            Property(p => p.Cor).HasMaxLength(50).IsRequired().HasColumnName("Cor");

            HasRequired(p => p.Usuario);
        }
    }
}
