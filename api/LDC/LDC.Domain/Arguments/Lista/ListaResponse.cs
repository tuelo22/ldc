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

        public bool Publica { get; set; }

        public Guid UsuarioId { get; set; }

        public double ValorTotal { get; set; }

        public double ValorComprado { get; set; }

        public int QuantidadeItens { get; set; }

        public int QuantidadeComprada { get; set; }

        public static explicit operator ListaResponse(Entities.Lista entidade)
        {
            return new ListaResponse()
            {
                Id = entidade.Id,
                Criacao = entidade.Criacao,
                Nome = entidade.Nome,
                Ordenacao = (int)entidade.Ordenacao,
                Publica = entidade.Publica,
                UsuarioId = entidade.ProprietarioId,
                ValorComprado = entidade.ValorComprado,
                ValorTotal = entidade.ValorTotal,
                QuantidadeItens = entidade.QuantidadeItens,
                QuantidadeComprada = entidade.QuantidadeComprada
            };
        }
    }
}
