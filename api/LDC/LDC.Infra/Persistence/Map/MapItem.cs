using LDC.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LDC.Infra.Persistence.Map
{
    public class MapItem : EntityTypeConfiguration<Item>
    {
        public MapItem()
        {
            ToTable("Item");

            HasKey(p => p.Id);

            Property(p => p.Pendente).IsRequired().HasColumnName("Pendente");

            HasRequired(p => p.Produto).WithMany(x => x.Items).HasForeignKey(s => s.ProdutoId);
            HasRequired(p => p.Lista).WithMany(x => x.Items).HasForeignKey(s => s.ListaId);
            HasRequired(p => p.Usuario).WithMany(x => x.Items).HasForeignKey(s => s.UsuarioId);
        }
    }
}
