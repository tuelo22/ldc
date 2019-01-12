using LDC.Domain.Entities.Base;
using LDC.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;

namespace LDC.Domain.Entities
{
    public class Produto : EntityBase
    {
        public string Nome { get; private set; }

        public Unidade Unidade { get; private set; }

        public Categoria Categoria { get; private set; }

        public Usuario Usuario { get; private set; }

        public bool Padrao { get; private set; }

        protected Produto()
        {

        }

        public Produto(string nome, Unidade unidade, Categoria categoria, Usuario usuario)
        {
            this.Nome = nome;
            this.Unidade = unidade;
            this.Categoria = categoria;
            this.Usuario = usuario;
            this.Padrao = false;

            Valida();
        }

        public void Alterar(string nome, Unidade unidade, Categoria categoria, Usuario usuario)
        {
            if (usuario != null)
            {
                new AddNotifications<Produto>(this).IfFalse(Usuario.Id.Equals(usuario.Id), Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(nome));
            }

            this.Nome = nome;
            this.Unidade = unidade;
            this.Categoria = categoria;
            this.Usuario = usuario;

            Valida();
        }

        protected override void Valida()
        {
            new AddNotifications<Produto>(this)
            .IfNullOrInvalidLength(x => x.Nome, 1, 100, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Nome", "1", "100"))
            .IfNull(x => x.Usuario, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuario"))
            .IfNull(x => x.Unidade, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Unidade"))
            .IfNull(x => x.Categoria, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Categoria"))
            ;

            if (Usuario != null)
            {
                AddNotifications(Usuario);
            }

            if (Unidade != null)
            {
                AddNotifications(Unidade);
            }

            if (Categoria != null)
            {
                AddNotifications(Categoria);
            }
        }
    }
}
