using LDC.Domain.Arguments.Produto;
using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Arguments.Lista;

namespace LDC.Domain.Arguments.Item
{
    public class AdicionarItemRequest
    {
        public ListaRequest Lista { get; private set; }

        public ProdutoRequest Produto { get; private set; }

        public UsuarioRequest Usuario { get; private set; }
    }
}
