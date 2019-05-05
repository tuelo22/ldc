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

            HasRequired(p => p.Lista);
            HasRequired(p => p.Produto);
            HasRequired(p => p.Usuario);

            HasMany(p => p.Precos).WithOptional(p => p.Item);
        }
    }
}
