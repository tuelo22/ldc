using LDC.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LDC.Infra.Persistence.Map
{
    public class MapProduto : EntityTypeConfiguration<Produto>
    {
        public MapProduto()
        {
            ToTable("Produto");

            HasKey(p => p.Id);

            Property(p => p.Nome).HasMaxLength(50).IsRequired().HasColumnName("Nome");

            HasRequired(p => p.Usuario);
            HasRequired(p => p.Categoria);
            HasRequired(p => p.Unidade);
        }
    }
}
