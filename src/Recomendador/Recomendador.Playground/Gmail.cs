using System.Net;
using System.Net.Mail;

namespace Recomendador.Playground
{
    public class Gmail
    {
        public void EnviarArtigo(Usuario usuario, Artigo artigo)
        {
            var smtpNome = "Recomendador";
            var smtpEmail = "r2d2@azys.com.br";
            var smtpSenha = "dfs465789ds47815";

            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(smtpEmail, smtpSenha)
            };

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(smtpEmail, smtpNome);
            mailMessage.Subject = artigo.Titulo;
            mailMessage.Body = artigo.Url;
            mailMessage.To.Add(usuario.Email);

            smtpClient.Send(mailMessage);
        }
    }
}
