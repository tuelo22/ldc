using LDC.Domain.Arguments.Categoria;
using LDC.Domain.Arguments.Unidade;
using LDC.Domain.Arguments.Usuario;

namespace LDC.Domain.Arguments.Produto
{
    public class AdicionarProdutoRequest
    {
        public string Nome { get; private set; }

        public UnidadeRequest Unidade { get; private set; }

        public CategoriaRequest Categoria { get; private set; }

        public UsuarioRequest Usuario { get; private set; }
    }
}
