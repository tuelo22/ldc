using LDC.Domain.Entities.Base;
using LDC.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;

namespace LDC.Domain.Entities
{
    public class Preco : EntityBase
    {
        public Estabelecimento Estabelecimento { get; set; }

        public double Valor { get; set; }

        public Usuario Usuario { get; set; }

        public Item Item { get; set; }

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
        }
    }
}
