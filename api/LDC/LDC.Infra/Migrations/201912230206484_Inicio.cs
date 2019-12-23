namespace LDC.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Padrao = c.Boolean(nullable: false),
                        Cor = c.String(nullable: false, maxLength: 50, unicode: false),
                        UsuarioId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Padrao = c.Boolean(nullable: false),
                        UnidadeId = c.Guid(nullable: false),
                        CategoriaId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.Unidade", t => t.UnidadeId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UnidadeId)
                .Index(t => t.CategoriaId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Pendente = c.Boolean(nullable: false),
                        ListaId = c.Guid(nullable: false),
                        ProdutoId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lista", t => t.ListaId)
                .ForeignKey("dbo.Produto", t => t.ProdutoId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.ListaId)
                .Index(t => t.ProdutoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Lista",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Criacao = c.DateTime(nullable: false, precision: 0),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ordenacao = c.Int(nullable: false),
                        Publica = c.Boolean(nullable: false),
                        PermiteOutrosEditarem = c.Boolean(nullable: false),
                        Publica1 = c.Boolean(nullable: false),
                        ValorTotal = c.Double(nullable: false),
                        ValorComprado = c.Double(nullable: false),
                        QuantidadeItens = c.Int(nullable: false),
                        QuantidadeComprada = c.Int(nullable: false),
                        ProprietarioId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.ProprietarioId)
                .Index(t => t.ProprietarioId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PrimeiroNome = c.String(nullable: false, maxLength: 50, unicode: false),
                        UltimoNome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(nullable: false, maxLength: 200, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true, name: "UK1_USUARIO");
            
            CreateTable(
                "dbo.Estabelecimento",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Bairro = c.String(nullable: false, maxLength: 50, unicode: false),
                        Cidade = c.String(nullable: false, maxLength: 50, unicode: false),
                        Estado = c.String(nullable: false, maxLength: 25, unicode: false),
                        Numero = c.String(nullable: false, maxLength: 12, unicode: false),
                        Complemento = c.String(nullable: false, maxLength: 100, unicode: false),
                        Rua = c.String(nullable: false, maxLength: 100, unicode: false),
                        Cep = c.String(nullable: false, maxLength: 12, unicode: false),
                        Longitude = c.String(nullable: false, maxLength: 40, unicode: false),
                        Latitude = c.String(nullable: false, maxLength: 40, unicode: false),
                        Padrao = c.Boolean(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Preco",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Valor = c.Double(nullable: false),
                        EstabelecimentoId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        ItemId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estabelecimento", t => t.EstabelecimentoId)
                .ForeignKey("dbo.Item", t => t.ItemId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.EstabelecimentoId)
                .Index(t => t.UsuarioId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Unidade",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Sigla = c.String(nullable: false, maxLength: 2, unicode: false),
                        CasasDecimais = c.Int(nullable: false),
                        Padrao = c.Boolean(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.UsuarioLista",
                c => new
                    {
                        Usuario_Id = c.Guid(nullable: false),
                        Lista_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuario_Id, t.Lista_Id })
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id)
                .ForeignKey("dbo.Lista", t => t.Lista_Id)
                .Index(t => t.Usuario_Id)
                .Index(t => t.Lista_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categoria", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Produto", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Produto", "UnidadeId", "dbo.Unidade");
            DropForeignKey("dbo.Item", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Item", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.Item", "ListaId", "dbo.Lista");
            DropForeignKey("dbo.Lista", "ProprietarioId", "dbo.Usuario");
            DropForeignKey("dbo.Unidade", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioLista", "Lista_Id", "dbo.Lista");
            DropForeignKey("dbo.UsuarioLista", "Usuario_Id", "dbo.Usuario");
            DropForeignKey("dbo.Estabelecimento", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Preco", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Preco", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Preco", "EstabelecimentoId", "dbo.Estabelecimento");
            DropForeignKey("dbo.Produto", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.UsuarioLista", new[] { "Lista_Id" });
            DropIndex("dbo.UsuarioLista", new[] { "Usuario_Id" });
            DropIndex("dbo.Unidade", new[] { "UsuarioId" });
            DropIndex("dbo.Preco", new[] { "ItemId" });
            DropIndex("dbo.Preco", new[] { "UsuarioId" });
            DropIndex("dbo.Preco", new[] { "EstabelecimentoId" });
            DropIndex("dbo.Estabelecimento", new[] { "UsuarioId" });
            DropIndex("dbo.Usuario", "UK1_USUARIO");
            DropIndex("dbo.Lista", new[] { "ProprietarioId" });
            DropIndex("dbo.Item", new[] { "UsuarioId" });
            DropIndex("dbo.Item", new[] { "ProdutoId" });
            DropIndex("dbo.Item", new[] { "ListaId" });
            DropIndex("dbo.Produto", new[] { "UsuarioId" });
            DropIndex("dbo.Produto", new[] { "CategoriaId" });
            DropIndex("dbo.Produto", new[] { "UnidadeId" });
            DropIndex("dbo.Categoria", new[] { "UsuarioId" });
            DropTable("dbo.UsuarioLista");
            DropTable("dbo.Unidade");
            DropTable("dbo.Preco");
            DropTable("dbo.Estabelecimento");
            DropTable("dbo.Usuario");
            DropTable("dbo.Lista");
            DropTable("dbo.Item");
            DropTable("dbo.Produto");
            DropTable("dbo.Categoria");
        }
    }
}
