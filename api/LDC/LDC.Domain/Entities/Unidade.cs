using LDC.Domain.Entities.Base;
using LDC.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;

namespace LDC.Domain.Entities
{
    public class Unidade : EntityBase
    {
        public string Nome { get; private set; }

        public string Sigla { get; private set; }

        public int CasasDecimais { get; private set; }

        public bool Padrao { get; private set; }

        public Usuario Usuario { get; private set; }

        protected Unidade()
        {

        }

        protected override void Valida()
        {
            new AddNotifications<Unidade>(this)
                .IfNullOrInvalidLength(x => x.Sigla,2,2, Message.X0_E_OBRIGATORIA_E_DEVE_CONTER_X1_CARACTERES.ToFormat("Sigla", "2"))
                .IfNullOrInvalidLength(x => x.Nome, 1, 30, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Nome", "1", "30"))
                .IfGreaterThan(x => x.CasasDecimais, 5, Message.A_X0_DEVE_SER_MENOR_OU_IGUAL_A_X1.ToFormat("Casas Decimais", "5 0,00000"))
                .IfNull(x => x.Usuario, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuario"));
        }

        public Unidade(string nome, string sigla, int casasdecimais, Usuario usuario)
        {
            this.Nome = nome;
            this.Sigla = sigla;
            this.CasasDecimais = casasdecimais;
            this.Usuario = usuario;
            this.Padrao = false;

            Valida();                
        }

        public void Alterar (string nome, string sigla, int casasdecimais, Usuario usuario)
        {
            if (usuario != null)
            {
                new AddNotifications<Unidade>(this).IfFalse(Usuario.Id.Equals(usuario.Id), Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(nome));
            }

            this.Nome = nome;
            this.Sigla = sigla;
            this.CasasDecimais = casasdecimais;
            this.Usuario = usuario;

            Valida();
        }

    }
}
