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

            HasRequired(p => p.Unidade).WithMany(x => x.Produtos).HasForeignKey(s => s.UnidadeId);
            HasRequired(p => p.Categoria).WithMany(x => x.Produtos).HasForeignKey(s => s.CategoriaId);
            HasRequired(p => p.Usuario).WithMany(x => x.Produtos).HasForeignKey(s => s.UsuarioId);
        }
    }
}
