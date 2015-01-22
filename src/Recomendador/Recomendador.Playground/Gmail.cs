using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Recomendador.Playground
{
    public class Gmail
    {
        public void EnviarArtigo(string usuario, string titulo, string url)
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
            mailMessage.Subject = titulo;
            mailMessage.Body = url;
            mailMessage.To.Add(usuario);

            smtpClient.Send(mailMessage);
        }
    }
}
