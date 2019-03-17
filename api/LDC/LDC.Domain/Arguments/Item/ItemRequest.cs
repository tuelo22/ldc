using LDC.Domain.Arguments.Lista;
using LDC.Domain.Arguments.Produto;
using LDC.Domain.Arguments.Usuario;
using System;

namespace LDC.Domain.Arguments.Item
{
    public class ItemRequest
    {
        public Guid Id { get; set; }

        public ListaRequest Lista { get; private set; }

        public ProdutoRequest Produto { get; private set; }

        public UsuarioRequest Usuario { get; private set; }

    }
}
