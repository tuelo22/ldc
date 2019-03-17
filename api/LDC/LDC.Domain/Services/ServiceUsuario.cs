using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Domain.Resources;
using LDC.Domain.ValueObjects;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDC.Domain.Services
{
    public class ServiceUsuario : Notifiable, IServiceUsuario
    {
        private readonly IRepositoryUsuario _repositoryUsuario;

        public ServiceUsuario()
        {

        }

        public ServiceUsuario(IRepositoryUsuario repositoryUsuario)
        {
            _repositoryUsuario = repositoryUsuario;
        }

        public AdicionarUsuarioResponse Adicionar(AdicionarUsuarioRequest request)
        {
            if (request == null)
            {
                AddNotification("AdicionarUsuarioRequest", Message.X0_E_OBRIGATORIO.ToFormat("AdicionarUsuarioRequest"));

                return null;
            }

            var email = new Email(request.Email);
            var nome = new Nome(request.PrimeiroNome, request.UltimoNome);
            var usuario = new Usuario(nome, email, request.Senha);

            AddNotifications(usuario);

            if (_repositoryUsuario.Existe(x => x.Email.Endereco == request.Email))
            {
                AddNotification("E-mail", Message.JA_EXISTE_UMA_X0_CHAMADA_X1.ToFormat("e-mail", request.Email));
            }

            if (this.IsInvalid())
            {
                return null;
            }

            usuario = _repositoryUsuario.Adicionar(usuario);

            return (AdicionarUsuarioResponse)usuario;

        }

        public ResponseBase Alterar(AlterarUsuarioRequest request)
        {
            throw new NotImplementedException();
        }

        public AutenticarUsuarioResponse Autenticar(AutenticarUsuarioRequest request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase Desativar(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioResponse> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
