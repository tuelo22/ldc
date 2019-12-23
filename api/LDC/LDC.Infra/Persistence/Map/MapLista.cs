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
            Property(p => p.ValorTotal).IsRequired().HasColumnName("ValorTotal"); 
            Property(p => p.ValorComprado).IsRequired().HasColumnName("ValorComprado"); 
            Property(p => p.QuantidadeItens).IsRequired().HasColumnName("QuantidadeItens");
            Property(p => p.QuantidadeComprada).IsRequired().HasColumnName("QuantidadeComprada");

            HasRequired(p => p.Proprietario).WithMany(x => x.Listas).HasForeignKey(s => s.ProprietarioId);

            HasMany(p => p.Usuarios).WithMany(x => x.ListasRelacionadas).Map(x => x.ToTable("UsuarioLista"));

        }
    }
}
