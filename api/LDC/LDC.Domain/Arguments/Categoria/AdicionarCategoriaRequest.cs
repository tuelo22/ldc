using LDC.Domain.Arguments.Usuario;

namespace LDC.Domain.Arguments.Categoria
{
    public class AdicionarCategoriaRequest
    {
        public string Nome { get; set; }

        public bool Padrao { get; set; }

        public string Cor { get; set; }

        public UsuarioRequest Usuario { get; set; }
    }
}
