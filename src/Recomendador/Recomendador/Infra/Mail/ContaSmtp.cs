namespace Recomendador.Infra.Mail
{
    public class ContaSmtp
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public ContaSmtp(string nome, string email, string senha)
        {
            // TODO: Validação!

            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
        }
    }
}
