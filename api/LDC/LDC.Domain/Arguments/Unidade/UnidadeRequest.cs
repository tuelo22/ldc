using LDC.Domain.Arguments.Usuario;
using System;

namespace LDC.Domain.Arguments.Unidade
{
    public class UnidadeRequest
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public int CasasDecimais { get; set; }

        public UsuarioRequest Usuario { get; set; }
    }
}
