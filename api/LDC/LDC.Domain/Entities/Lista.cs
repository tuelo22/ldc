using LDC.Domain.Entities.Base;
using LDC.Domain.Enum;
using LDC.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Entities
{
    public class Lista : EntityBase
    {
        public DateTime Criacao { get; private set; }

        public string Nome { get; private set; }

        public EnumOrdenacao Ordenacao { get; private set; }

        public bool Compartilhada { get; private set; }

        public bool PermiteOutrosEditarem { get; private set; }

        public bool Publica { get; private set; }

        public double ValorTotal { get; private set; }

        public double ValorComprado { get; private set; }

        public int QuantidadeItens { get; private set; }

        public int QuantidadeComprada { get; private set; }

        public Guid ProprietarioId { get; private set; }

        public virtual Usuario Proprietario { get; private set; }

        public virtual ICollection<Item> Items { get; private set; }

        public virtual ICollection<Usuario> Usuarios { get; private set; }

        protected Lista()
        {
            Items = new List<Item>();
            Usuarios = new List<Usuario>();
        }

        public Lista(DateTime criacao, string nome, Usuario usuario, bool publica)
        {
            this.Items = new List<Item>();
            this.Usuarios = new List<Usuario>();

            this.Usuarios.Add(usuario);
            this.ProprietarioId = usuario.Id;

            this.Criacao = criacao;
            this.Nome = nome;
            this.Ordenacao = EnumOrdenacao.Criacao;
            this.Proprietario = usuario;
            this.Compartilhada = false;
            this.PermiteOutrosEditarem = false;
            this.Publica = publica;
            this.ValorTotal = 0;
            this.ValorComprado = 0;
            this.QuantidadeItens = 0;
            this.QuantidadeComprada = 0;

            Valida();
        }

        protected override void Valida()
        {
            new AddNotifications<Lista>(this)
                .IfNullOrInvalidLength(x => x.Nome, 1, 50, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Nome", "1", "50"))
                .IfNull(x => x.Criacao, Message.X0_E_OBRIGATORIA.ToFormat("Data de Criação"))
                .IfNull(x => x.Proprietario, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuario"))
                .IfNull(x => x.Items, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Items"));

            if (IsValid())
            {
                this.ProprietarioId = Proprietario.Id;
            }
        }

        public void AutalizaTotalizadores(double valorTotal, double valorComprado, int quantidadeItens, int quantidadeComprada)
        {
            this.ValorTotal = valorTotal;
            this.ValorComprado = valorComprado;
            this.QuantidadeItens = quantidadeItens;
            this.QuantidadeComprada = quantidadeComprada;
        }

        public void Alterar(DateTime criacao, string nome, Usuario proprietario, bool publica)
        {
            if (proprietario != null)
            {
                new AddNotifications<Lista>(this).IfFalse(Proprietario.Id.Equals(proprietario.Id) || (Compartilhada && PermiteOutrosEditarem), Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(nome));
            }

            this.Criacao = criacao;
            this.Nome = nome;
            this.Ordenacao = EnumOrdenacao.Criacao;
            this.Proprietario = proprietario;
            this.Publica = publica;

            Valida();
        }
    }
}
