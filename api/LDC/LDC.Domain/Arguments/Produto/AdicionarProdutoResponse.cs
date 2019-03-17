using System;

namespace LDC.Domain.Arguments.Produto
{
    public class AdicionarProdutoResponse
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public static explicit operator AdicionarProdutoResponse(Entities.Estabelecimento entidade)
        {
            return new AdicionarProdutoResponse()
            {
                Id = entidade.Id,
                Message = Resources.Message.OPERACAO_REALIZADA_COM_SUCESSO
            };
        }
    }
}
