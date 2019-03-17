using LDC.Domain.Arguments.Estabelecimento;
using LDC.Domain.Arguments.Item;
using LDC.Domain.Arguments.Usuario;
using System;

namespace LDC.Domain.Arguments.Preco
{
    public class PrecoResponse
    {
        public Guid Id { get; set; }

        public EstabelecimentoResponse Estabelecimento { get; set; }

        public double Valor { get; set; }

        public UsuarioResponse Usuario { get; set; }

        public ItemResponse Item { get; set; }

        public static explicit operator PrecoResponse(Entities.Preco entidade)
        {
            return new PrecoResponse()
            {
                Id = entidade.Id,
                Estabelecimento = (EstabelecimentoResponse)entidade.Estabelecimento,
                Item = (ItemResponse)entidade.Item,
                Usuario = (UsuarioResponse)entidade.Usuario
            };
        }
    }
}
