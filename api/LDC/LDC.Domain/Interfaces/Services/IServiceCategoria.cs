using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Categoria;
using LDC.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Interfaces.Repositories
{
    public interface IServiceCategoria : IServiceBase
    {
        AdicionarCategoriaResponse Adicionar(AdicionarCategoriaRequest request);

        ResponseBase Alterar(AlterarCategoriaRequest request);

        IEnumerable<CategoriaResponse> Listar();

        ResponseBase Desativar(Guid Id);
    }
}
