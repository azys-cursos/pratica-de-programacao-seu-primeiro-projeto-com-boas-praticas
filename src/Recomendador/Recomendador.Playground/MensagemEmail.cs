using System.Net.Mail;

namespace Recomendador.Playground
{
    public class MensagemEmail
    {
        public Usuario Usuario { get; set; }

        public Artigo Artigo { get; set; }

        public MensagemEmail(Usuario usuario, Artigo artigo)
        {
            // TODO: Validação!

            this.Usuario = usuario;
            this.Artigo = artigo;
        }

        public MailMessage ObterMailMessage()
        {
            var mailMessage = new MailMessage
            {
                Subject = this.Artigo.Titulo,
                Body = this.Artigo.Url
            };

            mailMessage.To.Add(this.Usuario.Email);

            return mailMessage;
        }
    }
}