using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Preco;
using LDC.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Interfaces.Services
{
    public interface IServicePreco : IServiceBase
    {
        AdicionarPrecoResponse Adicionar(AdicionarPrecoRequest request);

        ResponseBase Alterar(AlterarPrecoRequest request);

        IEnumerable<PrecoResponse> Listar();

        ResponseBase Desativar(Guid Id);
    }
}
