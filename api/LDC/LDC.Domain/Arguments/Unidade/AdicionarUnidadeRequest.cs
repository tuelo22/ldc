using LDC.Domain.Arguments.Usuario;

namespace LDC.Domain.Arguments.Unidade
{
    public class AdicionarUnidadeRequest
    {
        public string Nome { get; set; }

        public string Sigla { get; set; }

        public int CasasDecimais { get; set; }

        public UsuarioRequest Usuario { get; set; }
    }
}
