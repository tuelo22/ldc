using System;

namespace LDC.Domain.Arguments.Unidade
{
    public class AdicionarUnidadeResponse
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public static explicit operator AdicionarUnidadeResponse(Entities.Estabelecimento entidade)
        {
            return new AdicionarUnidadeResponse()
            {
                Id = entidade.Id,
                Message = Resources.Message.OPERACAO_REALIZADA_COM_SUCESSO
            };
        }
    }
}
