using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Categoria;
using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Interfaces.Services
{
    public interface IServiceCategoria : IServiceBase
    {
        AdicionarCategoriaResponse Adicionar(AdicionarCategoriaRequest request);

        ResponseBase Alterar(AlterarCategoriaRequest request);

        IEnumerable<CategoriaResponse> Listar(UsuarioRequest request);

        ResponseBase Desativar(Guid Id, UsuarioRequest request);
    }
}
