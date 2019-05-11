using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Interfaces.Services
{
    public interface IServiceUsuario : IServiceBase
    {
        AutenticarUsuarioResponse Autenticar(AutenticarUsuarioRequest request);

        AdicionarUsuarioResponse Adicionar(AdicionarUsuarioRequest request);

        ResponseBase Alterar(AlterarUsuarioRequest request);

        IEnumerable<UsuarioResponse> Listar();

        ResponseBase Desativar(Guid Id);
    }
}
