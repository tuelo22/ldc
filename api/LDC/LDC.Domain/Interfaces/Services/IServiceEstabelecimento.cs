using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Estabelecimento;
using LDC.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Interfaces.Services
{
    public interface IServiceEstabelecimento : IServiceBase
    {
        AdicionarEstabelecimentoResponse Adicionar(AdicionarEstabelecimentoRequest request);

        ResponseBase Alterar(AlterarEstabelecimentoRequest request);

        IEnumerable<EstabelecimentoResponse> Listar();

        ResponseBase Desativar(Guid Id);
    }
}
