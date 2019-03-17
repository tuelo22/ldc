using LDC.Domain.Arguments.Lista;
using LDC.Domain.Arguments.Produto;
using LDC.Domain.Arguments.Usuario;
using System;

namespace LDC.Domain.Arguments.Item
{
    public class ItemResponse
    {
        public Guid Id { get; set; }

        public ListaResponse Lista { get; private set; }

        public ProdutoResponse Produto { get; private set; }

        public UsuarioResponse Usuario { get; private set; }

        public static explicit operator ItemResponse(Entities.Item entidade)
        {
            return new ItemResponse()
            {
                Id = entidade.Id,
                Lista = (ListaResponse) entidade.Lista,
                Produto = (ProdutoResponse) entidade.Produto,
                Usuario = (UsuarioResponse) entidade.Usuario
            };
        }
    }
}
