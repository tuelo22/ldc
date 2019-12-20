using LDC.Domain.Entities.Base;
using LDC.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Entities
{
    public class Item : EntityBase
    {
        public bool Pendente { get; private set; }

        public Guid ListaId { get; private set; }

        public virtual Lista Lista { get; private set; }

        public Guid ProdutoId { get; private set; }

        public virtual Produto Produto { get; private set; }

        public Guid UsuarioId { get; private set; }

        public virtual Usuario Usuario { get; private set; }

        public virtual ICollection<Preco> Precos { get; private set; }

        protected Item()
        {
            Precos = new List<Preco>();
        }

        public Item(Produto produto, Usuario usuario, Lista lista)
        {
            Precos = new List<Preco>();

            this.Produto = produto;
            this.Pendente = true;
            this.Usuario = usuario;
            this.Lista = lista;
            this.ListaId = lista.Id;
            this.ProdutoId = produto.Id;
            this.UsuarioId = Usuario.Id;

            Valida();
        }

        public void Alterar(Produto produto, Usuario usuario, bool pendente, Lista lista)
        {
            if (usuario != null && !lista.PermiteOutrosEditarem)
            {
                new AddNotifications<Item>(this).IfFalse(Usuario.Id.Equals(usuario.Id), Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(produto.Nome));
            }

            this.Produto = produto;
            this.Pendente = pendente;
            this.Usuario = usuario;

            Valida();
        }

        protected override void Valida()
        {
            new AddNotifications<Item>(this)
                .IfNull(x => x.Lista, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Lista"))
                .IfNull(x => x.Usuario, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuario"))
                .IfNull(x => x.Produto, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Produto"));

            if (Usuario != null)
            {
                AddNotifications(Usuario);
            }

            if (IsValid())
            {
                this.ListaId = Lista.Id;
                this.ProdutoId = Produto.Id;
                this.UsuarioId = Usuario.Id;
            }
        }
    }
}
