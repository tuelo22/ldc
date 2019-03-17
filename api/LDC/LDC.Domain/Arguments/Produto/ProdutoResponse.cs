using LDC.Domain.Arguments.Categoria;
using LDC.Domain.Arguments.Unidade;
using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Entities;
using System;

namespace LDC.Domain.Arguments.Produto
{
    public class ProdutoResponse
    {
        public Guid Id { get; set; }

        public string Nome { get; private set; }

        public UnidadeResponse Unidade { get; private set; }

        public CategoriaResponse Categoria { get; private set; }

        public UsuarioResponse Usuario { get; private set; }

        public bool Padrao { get; private set; }

        public static explicit operator ProdutoResponse(Entities.Produto entidade)
        {
            return new ProdutoResponse()
            {
                Id = entidade.Id,
                Nome = entidade.Nome,
                Unidade = (UnidadeResponse) entidade.Unidade,
                Categoria = (CategoriaResponse) entidade.Categoria,
                Usuario = (UsuarioResponse) entidade.Usuario,
                Padrao = entidade.Padrao
            };
        }
    }
}
