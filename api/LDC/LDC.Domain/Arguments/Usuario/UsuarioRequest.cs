using System;

namespace LDC.Domain.Arguments.Usuario
{
    public class UsuarioRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string NomeCompleto { get; set; }
    }
}
