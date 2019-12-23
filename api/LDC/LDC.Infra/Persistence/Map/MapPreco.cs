using LDC.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LDC.Infra.Persistence.Map
{
    public class MapPreco : EntityTypeConfiguration<Preco>
    {
        public MapPreco()
        {
            ToTable("Preco");

            HasKey(p => p.Id);

            Property(p => p.Valor).IsRequired().HasColumnName("Valor");

            HasRequired(p => p.Estabelecimento).WithMany(x => x.Precos).HasForeignKey(s => s.EstabelecimentoId);
            HasRequired(p => p.Item).WithMany(x => x.Precos).HasForeignKey(s => s.ItemId);
            HasRequired(p => p.Usuario).WithMany(x => x.Precos).HasForeignKey(s => s.UsuarioId);
        }
    }
}
