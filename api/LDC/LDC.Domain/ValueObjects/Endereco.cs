using LDC.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;

namespace LDC.Domain.ValueObjects
{
    public class Endereco : Notifiable
    {
        protected Endereco()
        {

        }

        public Endereco(string bairro, string cidade, string estado, string numero, string complemento, string rua, string cep)
        {
            this.Bairro = bairro;
            this.Cidade = cidade;
            this.Estado = estado;
            this.Numero = numero;
            this.Complemento = complemento;
            this.Rua = rua;
            this.CEP = cep;

            Valida();
        }

        private void Valida()
        {
            new AddNotifications<Endereco>(this)
                .IfNullOrInvalidLength(x => x.Bairro, 6, 32, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Bairro", "6", "100"))
                .IfNullOrInvalidLength(x => x.Cidade, 6, 32, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Cidade", "6", "100"))
                .IfNullOrInvalidLength(x => x.Estado, 6, 32, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Estado", "2", "50"))
                .IfNullOrInvalidLength(x => x.Numero, 6, 32, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Numero", "0", "11"))
                .IfNullOrInvalidLength(x => x.Complemento, 6, 32, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Complemento", "6", "100"))
                .IfNullOrInvalidLength(x => x.Rua, 6, 32, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Rua", "1", "100"))
                .IfNullOrInvalidLength(x => x.CEP, 6, 32, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("CEP", "8", "8"));
        }

        public string Bairro { get; private set; }

        public string Cidade { get; private set; }

        public string Estado { get; private set; }

        public string Numero { get; private set; }

        public string Complemento { get; private set; }

        public string Rua { get; private set; }

        public string CEP { get; private set; }
    }
}
