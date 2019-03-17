using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Entities;
using System;

namespace LDC.Domain.Arguments.Unidade
{
    public class UnidadeResponse
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public int CasasDecimais { get; set; }

        public bool Padrao { get; set; }

        public UsuarioResponse Usuario { get; set; }

        public static explicit operator UnidadeResponse(Entities.Unidade entidade)
        {
            return new UnidadeResponse()
            {
                Id = entidade.Id,
                Nome = entidade.Nome,
                Sigla = entidade.Sigla,
                CasasDecimais = entidade.CasasDecimais,
                Padrao = entidade.Padrao,
                Usuario = (UsuarioResponse)entidade.Usuario
            };
        }
    }
}
