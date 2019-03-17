using LDC.Domain.Arguments.Categoria;
using LDC.Domain.Arguments.Unidade;
using LDC.Domain.Arguments.Usuario;
using System;

namespace LDC.Domain.Arguments.Produto
{
    public class ProdutoRequest
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public UnidadeRequest Unidade { get; set; }

        public CategoriaRequest Categoria { get; set; }

        public UsuarioRequest Usuario { get; set; }
    }
}
