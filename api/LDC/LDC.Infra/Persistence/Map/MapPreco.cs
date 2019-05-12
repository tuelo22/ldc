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
        }
    }
}
