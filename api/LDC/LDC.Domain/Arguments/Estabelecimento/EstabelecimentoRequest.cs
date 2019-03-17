using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Entities;
using System;

namespace LDC.Domain.Arguments.Estabelecimento
{
    public class EstabelecimentoRequest
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Rua { get; set; }

        public string CEP { get; set; }

        public UsuarioRequest Usuario { get; set; }
    }
}
