using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Lista;
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
    public class ServiceLista : Notifiable, IServiceLista
    {
        private readonly IRepositoryLista _repositoryLista;

        private readonly IRepositoryUsuario _repositoryUsuario;

        private readonly IRepositoryProduto _repositoryProduto;

        private readonly IRepositoryItem _repositoryItem;


        public ServiceLista(IRepositoryLista repositoryLista, 
                            IRepositoryUsuario repositoryUsuario, 
                            IRepositoryProduto repositoryProduto,
                            IRepositoryItem repositoryItem)
        {
            _repositoryLista = repositoryLista;
            _repositoryUsuario = repositoryUsuario;
            _repositoryProduto = repositoryProduto;
            _repositoryItem = repositoryItem;
        }

        public AdicionarListaResponse Adicionar(AdicionarListaRequest request)
        {
            if (request == null)
            {
                AddNotification("AdicionarListaRequest", Message.X0_E_OBRIGATORIO.ToFormat("AdicionarListaRequest"));

                return null;
            }

            if (request.IdUsuario == null)
            {
                AddNotification("Usuario", Message.X0_E_OBRIGATORIO.ToFormat("Usuario"));

                return null;
            }

            Usuario usuario = _repositoryUsuario.ObterPorId(request.IdUsuario);

            var Lista = new Lista(request.Criacao, request.Nome, usuario, request.Publica);

            AddNotifications(Lista);

            if (this.IsInvalid())
            {
                return null;
            }

            Lista = _repositoryLista.Adicionar(Lista);

            ICollection<Item> itens = new List<Item>();

            foreach (var item in request.Itens)
            {
                var produto = _repositoryProduto.ObterPorId(item.IdProduto);

                itens.Add(new Item(produto, usuario, Lista));
            }

            AddNotifications(itens);

            if (this.IsInvalid())
            {
                return null;
            }

            _repositoryItem.AdicionarLista(itens);

            return (AdicionarListaResponse)Lista;
        }

        public ResponseBase Alterar(AlterarListaRequest request)
        {
            if (request == null)
            {
                AddNotification("AlterarListaResponse", Message.X0_E_OBRIGATORIO.ToFormat("AlterarListaResponse"));

                return null;
            }

            if (request.IdUsuario == null)
            {
                AddNotification("Usuario", Message.X0_E_OBRIGATORIO.ToFormat("Usuario"));

                return null;
            }

            Lista Lista = _repositoryLista.ObterPorId(request.Id);

            if (Lista == null)
            {
                AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);

                return null;
            }

            Usuario usuario = _repositoryUsuario.ObterPorId(request.IdUsuario);

            Lista.Alterar(request.Criacao, request.Nome, usuario, request.Publica);

            AddNotifications(Lista);

            if (IsInvalid())
            {
                return null;
            }

            _repositoryLista.Editar(Lista);

            return new ResponseBase();
        }

        public ResponseBase Desativar(Guid Id, Guid IdUsuario)
        {
            Lista Lista = _repositoryLista.ObterPorId(Id);

            if (IdUsuario == null)
            {
                AddNotification("Usuario", Message.X0_E_OBRIGATORIO.ToFormat("Usuario"));

                return null;
            }

            if (Lista == null)
            {
                AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            Usuario usuario = _repositoryUsuario.ObterPorId(IdUsuario);

            if (usuario == null)
            {
                AddNotification("Usuario", Message.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            if (IdUsuario.Equals(Lista.Proprietario.Id))
            {
                AddNotification("Usuario", Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(Lista.Nome));
                return null;
            }

            Lista.Inativar();

            _repositoryLista.Editar(Lista);

            return new ResponseBase();
        }

        public IEnumerable<ListaResponse> Listar(Guid IdUsuario)
        {
            return _repositoryLista.Listar().ToList()
                .Select(Lista => (ListaResponse)Lista)
                .Where(c => c.UsuarioId == IdUsuario)
                .ToList();
        }
    }
}
