﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDC.Domain.Arguments.Estabelecimento
{
    public class AdicionarEstabelecimentoResponse
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public static explicit operator AdicionarEstabelecimentoResponse(Entities.Estabelecimento entidade)
        {
            return new AdicionarEstabelecimentoResponse()
            {
                Id = entidade.Id,
                Message = Resources.Message.OPERACAO_REALIZADA_COM_SUCESSO
            };
        }
    }
}
