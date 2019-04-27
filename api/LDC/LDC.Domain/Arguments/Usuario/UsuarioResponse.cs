using System;

namespace LDC.Domain.Arguments.Usuario
{
    public class UsuarioResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string NomeCompleto { get; set; }

        public static explicit operator UsuarioResponse(Entities.Usuario entidade)
        {
            return new UsuarioResponse()
            {
                Email = entidade.Email.Endereco,
                PrimeiroNome = entidade.Nome.PrimeiroNome,
                UltimoNome = entidade.Nome.UltimoNome,
                Id = entidade.Id,
                NomeCompleto = entidade.Nome.PrimeiroNome + " " + entidade.Nome.UltimoNome
            };
        }
    }
}
