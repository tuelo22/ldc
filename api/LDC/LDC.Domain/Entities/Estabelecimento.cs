using LDC.Domain.Entities.Base;
using LDC.Domain.Resources;
using LDC.Domain.ValueObjects;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Entities
{
    public class Estabelecimento : EntityBase
    {
        public string Nome { get; private set; }

        public Endereco Endereco { get; private set; }

        public string Longitude { get; private set; }

        public string Latitude { get; private set; }

        public bool Padrao { get; private set; }

        public Guid UsuarioId { get; private set; }

        public virtual Usuario Usuario { get; private set; }

        public virtual ICollection<Preco> Precos { get; private set; }

        protected Estabelecimento()
        {
            Precos = new List<Preco>();
        }

        public void Alterar(string nome, Endereco endereco, string longitude, string latitude, Usuario usuario)
        {
            if (usuario != null)
            {
                new AddNotifications<Estabelecimento>(this).IfFalse(Usuario.Id.Equals(usuario.Id), Message.NAO_E_POSSIVEL_EDITAR_PERTENCE_OUTRO_USUARIO.ToFormat(nome));
            }

            this.Usuario = usuario;
            this.Nome = nome;
            this.Endereco = endereco;
            this.Longitude = longitude;
            this.Latitude = latitude;

            Valida();
        }

        public Estabelecimento(string nome, Endereco endereco, string longitude, string latitude, Usuario usuario)
        {
            this.Precos = new List<Preco>();
            this.Usuario = usuario;
            this.Nome = nome;
            this.Endereco = endereco;
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Padrao = false;

            Valida();
        }

        protected override void Valida()
        {
            new AddNotifications<Estabelecimento>(this)
            .IfNullOrInvalidLength(x => x.Nome, 1, 100, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Nome", "1", "100"))
            .IfNull(x => x.Usuario, Message.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuario"));

            if (Usuario != null)
            {
                AddNotifications(Usuario);
            }

            if (Endereco != null)
            {
                AddNotifications(Endereco);
            }

            if (IsValid())
            {
                this.UsuarioId = Usuario.Id;
            }
        }
    }
}
