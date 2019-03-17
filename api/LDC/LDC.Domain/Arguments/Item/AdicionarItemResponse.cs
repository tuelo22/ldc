using System;

namespace LDC.Domain.Arguments.Item
{
    public class AdicionarItemResponse
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public static explicit operator AdicionarItemResponse(Entities.Estabelecimento entidade)
        {
            return new AdicionarItemResponse()
            {
                Id = entidade.Id,
                Message = Resources.Message.OPERACAO_REALIZADA_COM_SUCESSO
            };
        }
    }
}
