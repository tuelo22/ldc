using LDC.Domain.Entities.Base;
using LDC.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;

namespace LDC.Domain.Entities
{
    public class Categoria : EntityBase
    {
        public string Nome { get; private set; }

        public bool Padrao { get; private set; }

        public Usuario Usuario { get; private set; }

        public string Cor { get; private set; }

        protected Categoria()
        {

        }

        public Categoria( string nome, Usuario usuario, string cor)
        {
            this.Nome = nome;
            this.Usuario = usuario;
            this.Padrao = false;
            this.Cor = cor;

            Valida();
        }

        protected override void Valida()
        {
            new AddNotifications<Categoria>(this)
            .IfNullOrInvalidLength(x => x.Nome, 1, 100, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Nome", "1", "100"))
            .IfNullOrInvalidLength(x => x.Cor, 7, 7, Message.X0_E_OBRIGATORIA_E_DEVE_CONTER_X1_CARACTERES.ToFormat("Cor", "7"))
            .IfNull(x => x.Usuario, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuario"));

            if (Usuario != null) {
                AddNotifications(Usuario);
            }
        }

        public void Alterar(string nome, Usuario usuario, string cor)
        {
            if (usuario != null)
            {
                new AddNotifications<Categoria>(this).IfFalse(Usuario.Id.Equals(usuario.Id), Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(nome));
            }

            this.Nome = nome;
            this.Usuario = usuario;
            this.Cor = cor;

            Valida();
        }
    }
}
