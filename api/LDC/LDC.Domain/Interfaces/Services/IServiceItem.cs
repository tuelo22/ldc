using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Item;
using LDC.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Interfaces.Repositories
{
    public interface IServiceItem : IServiceBase
    {
        AdicionarItemResponse Adicionar(AdicionarItemRequest request);

        ResponseBase Alterar(AlterarItemRequest request);

        IEnumerable<ItemResponse> Listar();

        ResponseBase Desativar(Guid Id);

    }
}
