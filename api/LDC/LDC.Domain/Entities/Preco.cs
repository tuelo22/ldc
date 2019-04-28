using LDC.Domain.Entities.Base;
using LDC.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;

namespace LDC.Domain.Entities
{
    public class Preco : EntityBase
    {
        public double Valor { get; set; }

        public Guid EstabelecimentoId { get; private set; }

        public virtual Estabelecimento Estabelecimento { get; private set; }

        public Guid UsuarioId { get; private set; }

        public virtual Usuario Usuario { get; private set; }

        public Guid ItemId { get; private set; }

        public virtual Item Item { get; private set; }

        protected Preco()
        {
        }

        public Preco(Estabelecimento estabelecimento, double valor, Usuario usuario, Item item)
        {
            this.Estabelecimento = estabelecimento;
            this.Valor = valor;
            this.Usuario = usuario;
            this.Item = item;

            Valida();
        }

        public void Alterar(Estabelecimento estabelecimento, double valor, Usuario usuario, Item item)
        {
            if (usuario != null)
            {
                new AddNotifications<Preco>(this).IfFalse(Usuario.Id.Equals(usuario.Id), Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(usuario.Nome));
            }

            this.Estabelecimento = estabelecimento;
            this.Valor = valor;
            this.Usuario = usuario;
            this.Item = item;

            Valida();
        }

        protected override void Valida()
        {
            new AddNotifications<Preco>(this)
                        .IfLowerOrEqualsThan(x => x.Valor, 0, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Valor"))
                        .IfNull(x => x.Usuario, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuario"))
                        .IfNull(x => x.Estabelecimento, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Estabelecimento"))
                        .IfNull(x => x.Item, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Item"))
;

            if (Usuario != null)
            {
                AddNotifications(Usuario);
            }

            if (Estabelecimento != null)
            {
                AddNotifications(Estabelecimento);
            }

            if (Item != null)
            {
                AddNotifications(Item);
            }

            if (IsValid())
            {
                this.EstabelecimentoId = Estabelecimento.Id;
                this.UsuarioId = Usuario.Id;
                this.ItemId = Item.Id;
            }
        }
    }
}
