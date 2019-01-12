using LDC.Domain.Arguments.Usuario;
using System;

namespace LDC.Domain.Arguments.Categoria
{
    public class AlterarCategoriaRequest
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public bool Padrao { get; set; }

        public string Cor { get; set; }

        public UsuarioRequest Usuario { get; set; }
    }
}
