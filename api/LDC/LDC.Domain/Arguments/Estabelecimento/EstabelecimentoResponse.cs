using LDC.Domain.Arguments.Usuario;
using System;

namespace LDC.Domain.Arguments.Estabelecimento
{
    public class EstabelecimentoResponse
    {
        public Guid Id { get; set; }

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

        public static explicit operator EstabelecimentoResponse(Entities.Estabelecimento entidade)
        {
            return new EstabelecimentoResponse()
            {
                Id = entidade.Id,
                Nome = entidade.Nome,
                Padrao = entidade.Padrao,
                Usuario = (UsuarioResponse)entidade.Usuario,
                Longitude = entidade.Longitude,
                Latitude = entidade.Latitude,
                Bairro = entidade.Endereco.Bairro,
                Cidade = entidade.Endereco.Cidade,
                Estado = entidade.Endereco.Estado,
                Numero = entidade.Endereco.Numero,
                Complemento = entidade.Endereco.Complemento,
                Rua = entidade.Endereco.Rua,
                CEP = entidade.Endereco.CEP,
            };
        }
    }
}
