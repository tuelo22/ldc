using LDC.Domain.Arguments.Usuario;

namespace LDC.Domain.Arguments.Categoria
{
    public class CategoriaResponse
    {
        public string Nome { get; private set; }

        public bool Padrao { get; private set; }

        public UsuarioResponse Usuario { get; private set; }

        public string Cor { get; private set; }

        public static explicit operator CategoriaResponse(Entities.Categoria entidade)
        {
            return new CategoriaResponse()
            {
                Nome = entidade.Nome,
                Padrao = entidade.Padrao,
                Usuario = (UsuarioResponse)entidade.Usuario,
                Cor = entidade.Cor
            };
        }
    }
}
