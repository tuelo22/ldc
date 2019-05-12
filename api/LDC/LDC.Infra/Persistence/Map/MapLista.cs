using LDC.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LDC.Infra.Persistence.Map
{
    public class MapLista : EntityTypeConfiguration<Lista>
    {
        public MapLista()
        {
            ToTable("Lista");

            HasKey(p => p.Id);

            Property(p => p.Criacao).IsRequired().HasColumnName("Criacao");
            Property(p => p.Nome).HasMaxLength(50).IsRequired().HasColumnName("Nome");
            Property(p => p.Ordenacao).IsRequired().HasColumnName("Ordenacao");
            Property(p => p.Compartilhada).IsRequired().HasColumnName("Compartilhada");
            Property(p => p.Compartilhada).IsRequired().HasColumnName("PermiteOutrosEditarem");
            Property(p => p.Compartilhada).IsRequired().HasColumnName("Publica");

            HasRequired(p => p.Proprietario);

            HasMany(p => p.Usuarios).WithMany(x => x.Listas).Map(x => x.ToTable("UsuarioLista"));

        }
    }
}
