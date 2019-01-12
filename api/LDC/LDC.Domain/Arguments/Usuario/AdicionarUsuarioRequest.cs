using LDC.Domain.Interfaces.Arguments;

namespace LDC.Domain.Arguments.Usuario
{
    public class AdicionarUsuarioRequest : IRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string IMEI { get; set; }
    }
}
