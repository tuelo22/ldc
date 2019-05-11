using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Categoria;
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
    public class ServiceCategoria : Notifiable, IServiceCategoria
    {
        private readonly IRepositoryUsuario _repositoryUsuario;

        private readonly IRepositoryCategoria _repositoryCategoria;

        public ServiceCategoria(IRepositoryUsuario repositoryUsuario, IRepositoryCategoria repositoryCategoria)
        {
            _repositoryUsuario = repositoryUsuario;
            _repositoryCategoria = repositoryCategoria;
        }

        public AdicionarCategoriaResponse Adicionar(AdicionarCategoriaRequest request)
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

            var categoria = new Categoria(request.Nome, usuario, request.Cor);

            AddNotifications(categoria);

            if (this.IsInvalid())
            {
                return null;
            }

            categoria = _repositoryCategoria.Adicionar(categoria);

            return (AdicionarCategoriaResponse)categoria;
        }

        public ResponseBase Alterar(AlterarCategoriaRequest request)
        {
            if (request == null)
            {
                AddNotification("AlterarCategoriaResponse", Message.X0_E_OBRIGATORIO.ToFormat("AlterarCategoriaResponse"));

                return null;
            }

            if (request.Usuario == null)
            {
                AddNotification("Usuario", Message.X0_E_OBRIGATORIO.ToFormat("Usuario"));

                return null;
            }

            Categoria categoria = _repositoryCategoria.ObterPorId(request.Id);

            if (categoria == null)
            {
                AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);

                return null;
            }

            Usuario usuario = _repositoryUsuario.ObterPorId(request.Usuario.Id);

            categoria.Alterar(request.Nome, usuario, request.Cor);

            AddNotifications(categoria);

            if (IsInvalid())
            {
                return null;
            }

            _repositoryCategoria.Editar(categoria);

            return new ResponseBase();
        }

        public ResponseBase Desativar(Guid Id, UsuarioRequest request)
        {
            Categoria categoria = _repositoryCategoria.ObterPorId(Id);

            if (categoria == null)
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

            if (usuario.Id.Equals(categoria.Usuario.Id))
            {
                AddNotification("Usuario", Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(categoria.Nome));
                return null;
            }

            categoria.Inativar();

            _repositoryCategoria.Editar(categoria);

            return new ResponseBase();
        }

        public IEnumerable<CategoriaResponse> Listar(UsuarioRequest request)
        {
            Usuario usuario = _repositoryUsuario.ObterPorId(request.Id);

            if (usuario == null)
            {
                AddNotification("Usuario", Message.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            return _repositoryCategoria.Listar().ToList()
                .Select(unidade => (CategoriaResponse)unidade)
                .Where(c => c.Usuario.Id.Equals(usuario.Id))
                .ToList();
        }
    }
}
