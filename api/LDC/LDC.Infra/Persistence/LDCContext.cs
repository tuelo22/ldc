using LDC.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MySql.Data.EntityFramework;

namespace LDC.Infra.Persistence
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LDCContext : DbContext
    {
        public LDCContext() : base("LDC")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<Usuario> Usuarios { get; set; }
        public IDbSet<Unidade> Unidades { get; set; }
        public IDbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Seta o Schema default
            //modelBuilder.HasDefaultSchema("Apoio");

            //Remove a pluralização dos nomes das tabelas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Remove exclusão em cascata
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //Setar para usar varchar ou invés de nvarchar
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));

            //Caso eu esqueça de informar o tamanho do campo ele irá colocar varchar de 100
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            //Mapeia as entidades
            #region Adiciona entidades mapeadas - Automaticamente via Assembly
            modelBuilder.Configurations.AddFromAssembly(typeof(LDCContext).Assembly);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
