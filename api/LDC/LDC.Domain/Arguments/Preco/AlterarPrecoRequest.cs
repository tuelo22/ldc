using LDC.Domain.Arguments.Estabelecimento;
using LDC.Domain.Arguments.Item;
using LDC.Domain.Arguments.Usuario;
using System;

namespace LDC.Domain.Arguments.Preco
{
    public class AlterarPrecoRequest
    {
        public Guid Id { get; set; }

        public EstabelecimentoRequest Estabelecimento { get; set; }

        public double Valor { get; set; }

        public UsuarioRequest Usuario { get; set; }

        public ItemRequest Item { get; set; }
    }
}
