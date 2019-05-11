using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Unidade;
using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Domain.Interfaces.Services;
using LDC.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LDC.Domain.Services
{
    public class ServiceUnidade : Notifiable, IServiceUnidade
    {
        private readonly IRepositoryUnidade _repositoryUnidade;

        private readonly IRepositoryUsuario _repositoryUsuario;

        public ServiceUnidade(IRepositoryUnidade repositoryUnidade, IRepositoryUsuario repositoryUsuario)
        {
            _repositoryUnidade = repositoryUnidade;
        }

        public AdicionarUnidadeResponse Adicionar(AdicionarUnidadeRequest request)
        {
            if (request == null)
            {
                AddNotification("AdicionarUnidadeRequest", Message.X0_E_OBRIGATORIO.ToFormat("AdicionarUnidadeRequest"));

                return null;
            }

            if (request.Usuario == null)
            {
                AddNotification("Usuario", Message.X0_E_OBRIGATORIO.ToFormat("Usuario"));

                return null;
            }

            Usuario usuario = _repositoryUsuario.ObterPorId(request.Usuario.Id);

            var Unidade = new Unidade(request.Nome, request.Sigla, request.CasasDecimais, usuario);

            AddNotifications(Unidade);

            if (this.IsInvalid())
            {
                return null;
            }

            Unidade = _repositoryUnidade.Adicionar(Unidade);

            return (AdicionarUnidadeResponse)Unidade;

        }

        public ResponseBase Alterar(AlterarUnidadeRequest request)
        {
            if (request == null)
            {
                AddNotification("AlterarUnidadeResponse", Message.X0_E_OBRIGATORIO.ToFormat("AlterarUnidadeResponse"));

                return null;
            }

            if (request.Usuario == null)
            {
                AddNotification("Usuario", Message.X0_E_OBRIGATORIO.ToFormat("Usuario"));

                return null;
            }

            Unidade Unidade = _repositoryUnidade.ObterPorId(request.Id);

            if (Unidade == null)
            {
                AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);

                return null;
            }

            Usuario usuario = _repositoryUsuario.ObterPorId(request.Usuario.Id);

            Unidade.Alterar(request.Nome, request.Sigla, request.CasasDecimais, usuario);

            AddNotifications(Unidade);

            if (IsInvalid())
            {
                return null;
            }

            _repositoryUnidade.Editar(Unidade);

            return new ResponseBase();
        }

        public ResponseBase Desativar(Guid Id, UsuarioRequest request)
        {
            Unidade unidade = _repositoryUnidade.ObterPorId(Id);

            if (request == null)
            {
                AddNotification("Usuario", Message.X0_E_OBRIGATORIO.ToFormat("Usuario"));

                return null;
            }

            if (unidade == null)
            {
                AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            Usuario usuario = _repositoryUsuario.ObterPorId(request.Id);

            if (usuario == null)
            {
                AddNotification("Usuario", Message.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            if (usuario.Id.Equals(unidade.Usuario.Id))
            {
                AddNotification("Usuario", Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(unidade.Nome));
                return null;
            }

            unidade.Inativar();

            _repositoryUnidade.Editar(unidade);

            return new ResponseBase();
        }

        public IEnumerable<UnidadeResponse> Listar(UsuarioRequest request)
        {
            Usuario usuario = _repositoryUsuario.ObterPorId(request.Id);

            if (usuario == null)
            {
                AddNotification("Usuario", Message.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            return _repositoryUnidade.Listar().ToList()
                .Select(unidade => (UnidadeResponse)unidade)
                .Where(c => c.Usuario.Id.Equals(usuario.Id))
                .ToList();


        }
    }
}
