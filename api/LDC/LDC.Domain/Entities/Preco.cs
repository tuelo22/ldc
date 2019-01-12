using LDC.Domain.Entities.Base;
using System;

namespace LDC.Domain.Entities
{
    public class Preco : EntityBase
    {
        public Guid ProdutoListado { get; set; }

        public Estabelecimento Estabelecimento { get; set; }

        public double Valor { get; set; }

        public Usuario Usuario { get; set; }

        public Item Item { get; set; }

        protected override void Valida()
        {
            throw new NotImplementedException();
        }
    }
}
