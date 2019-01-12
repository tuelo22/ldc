using System;

namespace LDC.Domain.Arguments.Categoria
{
    public class AdicionarCategoriaResponse
    {
       public Guid Id { get; set; }

       public string Message { get; set; }

       public static explicit operator AdicionarCategoriaResponse(Entities.Categoria entidade)
       {
           return new AdicionarCategoriaResponse()
           {
               Id = entidade.Id,
               Message = Resources.Message.OPERACAO_REALIZADA_COM_SUCESSO
           };
       }
    }
}
