using System;

namespace LDC.Domain.Arguments.Usuario
{
    public class AutenticarUsuarioResponse
    {
        public Guid Id { get; set; }

        public string PrimeiroNome { get; set; }

        public string Email { get; set; }

        public string IMEI { get; set; }

        public static explicit operator AutenticarUsuarioResponse(Entities.Usuario entidade)
        {
            return new AutenticarUsuarioResponse()
            {
                Id = entidade.Id,
                Email = entidade.Email.Endereco,
                PrimeiroNome = entidade.Nome.PrimeiroNome,
                IMEI = entidade.IMEI,
            };
        }
    }
}
