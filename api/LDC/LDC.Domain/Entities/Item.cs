using LDC.Domain.Entities.Base;
using LDC.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Collections.Generic;

namespace LDC.Domain.Entities
{
    public class Item : EntityBase
    {
        public Lista Lista { get; private set; }

        public Produto Produto { get; private set; }

        public bool Pendente { get; private set; }

        public bool Ativo { get; private set; }

        public Usuario Usuario { get; private set; }

        public List<Preco> Precos { get; private set; }

        protected Item()
        {

        }

        public Item(Produto produto, Usuario usuario, Lista lista)
        {
            this.Produto = produto;
            this.Pendente = true;
            this.Ativo = true;
            this.Usuario = usuario;
            this.Lista = lista;

            Valida();
        }

        public void Alterar(Produto produto, Usuario usuario, bool ativo, bool pendente, Lista lista)
        {
            if (usuario != null && !lista.PermiteOutrosEditarem)
            {
                new AddNotifications<Item>(this).IfFalse(Usuario.Id.Equals(usuario.Id), Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(produto.Nome));
            }

            this.Produto = produto;
            this.Pendente = pendente;
            this.Ativo = ativo;
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
        }

    }
}
