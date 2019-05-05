using LDC.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LDC.Infra.Persistence.Map
{
    public class MapUnidade : EntityTypeConfiguration<Unidade>
    {
        public MapUnidade()
        {
            ToTable("Unidade");

            HasKey(p => p.Id);

            Property(p => p.Nome).HasMaxLength(50).IsRequired().HasColumnName("Nome");
            Property(p => p.Sigla).HasMaxLength(2).IsRequired().HasColumnName("Sigla");
            Property(p => p.CasasDecimais).IsRequired().HasColumnName("CasasDecimais");

            HasRequired(p => p.Usuario);

            HasMany(p => p.Produtos).WithOptional(p => p.Unidade);
        }
    }
}
