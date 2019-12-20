using LDC.Domain.Arguments.Item;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Arguments.Lista
{
    public class AdicionarListaRequest
    {
        public DateTime Criacao { get; set; }

        public string Nome { get; set; }

        public int Ordenacao { get; set; }

        public Guid IdUsuario { get; set; }

        public bool Publica { get; set; }

        public List<ItemRequest> Itens { get; set; }

    }
}
