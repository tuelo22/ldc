using System;

namespace LDC.Domain.Arguments.Lista
{
    public class AdicionarListaResponse
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public static explicit operator AdicionarListaResponse(Entities.Lista entidade)
        {
            return new AdicionarListaResponse()
            {
                Id = entidade.Id,
                Message = Resources.Message.OPERACAO_REALIZADA_COM_SUCESSO
            };
        }
    }
}
