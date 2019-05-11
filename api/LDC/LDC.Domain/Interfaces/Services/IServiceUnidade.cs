using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Unidade;
using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Interfaces.Services
{
    public interface IServiceUnidade : IServiceBase
    {
        AdicionarUnidadeResponse Adicionar(AdicionarUnidadeRequest request);

        ResponseBase Alterar(AlterarUnidadeRequest request);

        IEnumerable<UnidadeResponse> Listar(UsuarioRequest Usuario);

        ResponseBase Desativar(Guid Id, UsuarioRequest Usuario);

    }
}
