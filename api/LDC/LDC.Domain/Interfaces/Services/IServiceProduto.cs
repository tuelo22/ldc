using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Produto;
using LDC.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Interfaces.Repositories
{
    public interface IServiceProduto : IServiceBase
    {
        AdicionarProdutoResponse Adicionar(AdicionarProdutoRequest request);

        ResponseBase Alterar(AlterarProdutoRequest request);

        IEnumerable<ProdutoResponse> Listar();

        ResponseBase Desativar(Guid Id);

    }
}
