﻿namespace LDC.Domain.Arguments.Usuario
{
    public class AlterarUsuarioRequest
    {
        public string Email { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string IMEI { get; set; }
    }
}