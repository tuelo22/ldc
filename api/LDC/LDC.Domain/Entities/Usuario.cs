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

        #region Listas
        public virtual ICollection<Categoria> Categorias { get; set; }
        public virtual ICollection<Unidade> Unidades { get; set; }
        public virtual ICollection<Estabelecimento> Estabelecimentos { get; set; }
        public virtual ICollection<Lista> Listas { get; set; }
        public virtual ICollection<Preco> Precos { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
        #endregion

        private void InicializaListas()
        {
            Categorias = new List<Categoria>();
            Unidades = new List<Unidade>();
            Estabelecimentos = new List<Estabelecimento>();
            Listas = new List<Lista>();
            Precos = new List<Preco>();
            Items = new List<Item>();
            Produtos = new List<Produto>();
        }

        protected Usuario()
        {
            InicializaListas();
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

            InicializaListas();
        }

        public Usuario(Nome nome, Email email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;

            Valida();

            if (IsValid())
            {
                Senha = Senha.ConvertToMD5();
            }

            InicializaListas();
        }

        public void Alterar(Nome nome, Email email)
        {
            this.Nome = nome;
            this.Email = email;

            Valida();
        }

        public override void Inativar()
        {
            base.Inativar();          

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
