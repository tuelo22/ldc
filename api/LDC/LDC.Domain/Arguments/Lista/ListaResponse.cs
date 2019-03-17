using LDC.Domain.Arguments.Usuario;
using System;

namespace LDC.Domain.Arguments.Lista
{
    public class ListaResponse
    {
        public Guid Id { get; set; }

        public DateTime Criacao { get; set; }

        public string Nome { get; set; }

        public int Ordenacao { get; set; }

        public UsuarioResponse Usuario { get; set; }

        public bool Publica { get; set; }

        public static explicit operator ListaResponse(Entities.Lista entidade)
        {
            return new ListaResponse()
            {
                Id = entidade.Id,
                Criacao = entidade.Criacao,
                Nome = entidade.Nome,
                Ordenacao = (int)entidade.Ordenacao,
                Usuario = (UsuarioResponse) entidade.Usuario,
                Publica = entidade.Publica
            };
        }
    }
}
