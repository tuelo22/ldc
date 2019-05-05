using LDC.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LDC.Infra.Persistence.Map
{
    public class MapEstabelecimento : EntityTypeConfiguration<Estabelecimento>
    {
        public MapEstabelecimento()
        {
            ToTable("Estabelecimento");

            HasKey(p => p.Id);

            Property(p => p.Nome).HasMaxLength(50).IsRequired().HasColumnName("Nome");
            Property(p => p.Endereco.Bairro).HasMaxLength(50).IsRequired().HasColumnName("Bairro");
            Property(p => p.Endereco.Cidade).HasMaxLength(50).IsRequired().HasColumnName("Cidade");
            Property(p => p.Endereco.Estado).HasMaxLength(25).IsRequired().HasColumnName("Estado");
            Property(p => p.Endereco.Numero).HasMaxLength(12).IsRequired().HasColumnName("Numero");
            Property(p => p.Endereco.Complemento).HasMaxLength(100).IsRequired().HasColumnName("Complemento");
            Property(p => p.Endereco.Rua).HasMaxLength(100).IsRequired().HasColumnName("Rua");
            Property(p => p.Endereco.CEP).HasMaxLength(12).IsRequired().HasColumnName("Cep");
            Property(p => p.Longitude).HasMaxLength(40).IsRequired().HasColumnName("Longitude");
            Property(p => p.Latitude).HasMaxLength(40).IsRequired().HasColumnName("Latitude");

            HasRequired(p => p.Usuario);

            HasMany(p => p.Precos).WithOptional(p => p.Estabelecimento);
        }
    }
}
