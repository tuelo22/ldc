using System;

namespace LDC.Domain.Arguments.Lista
{
    public class AlterarListaRequest
    {
        public Guid Id { get; set; }

        public DateTime Criacao { get; private set; }

        public string Nome { get; private set; }

        public int Ordenacao { get; private set; }

        public Guid IdUsuario { get; private set; }

        public bool Publica { get; private set; }
    }
}
