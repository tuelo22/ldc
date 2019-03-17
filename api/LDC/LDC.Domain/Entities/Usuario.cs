using LDC.Domain.Entities.Base;
using LDC.Domain.Extensions;
using LDC.Domain.Resources;
using LDC.Domain.ValueObjects;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Collections.Generic;

namespace LDC.Domain.Entities
{
    public class Usuario : EntityBase
    {  
        public Nome Nome { get; private set; }

        public string Senha { get; private set; }

        public Email Email { get; private set; }

        public bool Ativo { get; private set; }

        public List<Lista> Listas { get; private set; }

        protected Usuario()
        {

        }

        public Usuario(Email email, string senha)
        {
            Email = email;
            Senha = senha;

            Valida();

            if (IsInvalid())
            {
                Senha = Senha.ConvertToMD5();
            }
        }

        public Usuario(Nome nome, Email email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Ativo = true;

            Valida();

            if (IsValid())
            {
                Senha = Senha.ConvertToMD5();
            }
        }

        public void Alterar(Nome nome, Email email)
        {
            this.Nome = nome;
            this.Email = email;

            Valida();
        }

        public void Inativar()
        {
            Ativo = false;

            Nome.Desativar();
        }

        public override string ToString()
        {
            return this.Nome.PrimeiroNome + " " + this.Nome.UltimoNome;
        }

        protected override void Valida()
        {
            AddNotifications(Nome, Email);

            new AddNotifications<Usuario>(this)
                .IfNullOrInvalidLength(x => x.Senha, 6, 32, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Senha", "6", "32"));
        }
    }
}
