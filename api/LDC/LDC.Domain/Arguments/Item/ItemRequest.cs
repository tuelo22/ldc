using System;

namespace LDC.Domain.Arguments.Item
{
    public class ItemRequest
    {
        public Guid Id { get; set; }

        public Guid IdProduto { get; private set; }

        public Guid IdUsuario { get; private set; }

    }
}
