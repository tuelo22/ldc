using LDC.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace LDC.Infra.Persistence.Map
{
    public class MapUsuario : EntityTypeConfiguration<Usuario>
    {
        public MapUsuario()
        {
            ToTable("Usuario");

            HasKey(p => p.Id);

            Property(p => p.Email.Endereco).HasMaxLength(200).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UK1_USUARIO") { IsUnique = true })).HasColumnName("Email");
            Property(p => p.Nome.PrimeiroNome).HasMaxLength(50).IsRequired().HasColumnName("PrimeiroNome");
            Property(p => p.Nome.UltimoNome).HasMaxLength(50).IsRequired().HasColumnName("UltimoNome");
            Property(p => p.Ativo).IsRequired().HasColumnName("Ativo");
            Property(p => p.Senha).IsRequired();
            Property(p => p.Temporario).IsRequired();
            Property(p => p.DataCadastro).IsRequired();

            HasMany(p => p.ListasRelacionadas).WithMany(x => x.Usuarios).Map(x => x.ToTable("UsuarioLista"));
        }
    }
}
