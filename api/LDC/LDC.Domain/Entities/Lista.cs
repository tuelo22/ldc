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

        public Guid UsuarioId { get; private set; }

        public virtual Usuario Proprietario { get; private set; }

        public virtual ICollection<Item> Items { get; private set; }

        public virtual ICollection<Usuario> Usuarios { get; private set; }

        protected Lista()
        {
            Items = new List<Item>();
        }

        public Lista(DateTime criacao, string nome, Usuario usuario, List<Item> items, bool publica)
        {
            this.Items = new List<Item>();
            this.Usuarios = new List<Usuario>();

            this.Usuarios.Add(usuario);

            this.Criacao = criacao;
            this.Nome = nome;
            this.Ordenacao = EnumOrdenacao.Criacao;
            this.Proprietario = usuario;
            this.Items = items;
            this.Compartilhada = false;
            this.PermiteOutrosEditarem = false;
            this.Publica = publica;

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
                this.UsuarioId = Proprietario.Id;
            }
        }

        public void Alterar(DateTime criacao, string nome, Usuario proprietario, List<Item> items, bool publica, bool compartilhada, bool permiteOutrosEditarem)
        {
            if (proprietario != null)
            {
                new AddNotifications<Lista>(this).IfFalse(Proprietario.Id.Equals(proprietario.Id) || (compartilhada && permiteOutrosEditarem), Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(nome));
            }

            this.Criacao = criacao;
            this.Nome = nome;
            this.Ordenacao = EnumOrdenacao.Criacao;
            this.Proprietario = proprietario;
            this.Items = items;
            this.Compartilhada = compartilhada;
            this.PermiteOutrosEditarem = permiteOutrosEditarem;
            this.Publica = publica;

            Valida();
        }
    }
}
