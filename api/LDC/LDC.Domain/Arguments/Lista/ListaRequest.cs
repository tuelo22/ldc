using LDC.Domain.Arguments.Usuario;
using System;

namespace LDC.Domain.Arguments.Lista
{
    public class ListaRequest
    {
        public Guid Id { get; set; }

        public DateTime Criacao { get; set; }

        public string Nome { get; set; }

        public int Ordenacao { get; set; }

        public UsuarioRequest Usuario { get; set; }

        public bool Publica { get; set; }
    }
}
