using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Domain.Interfaces.Services;
using LDC.Domain.Resources;
using LDC.Domain.ValueObjects;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LDC.Domain.Services
{
    public class ServiceUsuario : Notifiable, IServiceUsuario
    {
        private readonly IRepositoryUsuario _repositoryUsuario;

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
            if (request == null)
            {
                AddNotification("AlterarJogadorResponse", Message.X0_E_OBRIGATORIO.ToFormat("AlterarJogadorResponse"));

                return null;
            }

            Usuario usuario = _repositoryUsuario.ObterPorId(request.Id);

            if (usuario == null)
            {
                AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);

                return null;
            }

            var nome = new Nome(request.PrimeiroNome, request.UltimoNome);

            var email = new Email(request.Email);

            usuario.Alterar(nome, email);

            AddNotifications(usuario);

            if (IsInvalid())
            {

                return null;
            }

            _repositoryUsuario.Editar(usuario);

            return new ResponseBase();
        }

        public AutenticarUsuarioResponse Autenticar(AutenticarUsuarioRequest request)
        {
            if (request == null)
            {
                AddNotification("AutenticarUsuarioRequest", Message.X0_E_OBRIGATORIO.ToFormat("AutenticarUsuarioRequest"));

                return null;
            }

            var email = new Email(request.Email);

            var usuario = new Usuario(email, request.Senha);

            AddNotifications(usuario, email);

            if (usuario.IsInvalid())
            {

                return null;
            }

            usuario = _repositoryUsuario.ObterPor(x => x.Email.Endereco == usuario.Email.Endereco && x.Senha == usuario.Senha && usuario.Ativo);

            if (usuario == null)
            {
                return null;
            }

            return (AutenticarUsuarioResponse)usuario;
        }

        public ResponseBase Desativar(Guid Id)
        {
            Usuario usuario = _repositoryUsuario.ObterPorId(Id);

            if (usuario == null)
            {
                AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            usuario.Inativar();

            _repositoryUsuario.Editar(usuario);

            return new ResponseBase();
        }

        public IEnumerable<UsuarioResponse> Listar()
        {
            return _repositoryUsuario.Listar().ToList().Select(jogador => (UsuarioResponse)jogador).ToList();
        }
    }
}
