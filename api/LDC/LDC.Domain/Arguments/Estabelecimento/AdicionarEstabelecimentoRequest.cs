using LDC.Domain.Arguments.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDC.Domain.Arguments.Estabelecimento
{
    public class AdicionarEstabelecimentoRequest
    {
        public string Nome { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public bool Padrao { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Rua { get; set; }

        public string CEP { get; set; }

        public UsuarioResponse Usuario { get; set; }
    }
}
