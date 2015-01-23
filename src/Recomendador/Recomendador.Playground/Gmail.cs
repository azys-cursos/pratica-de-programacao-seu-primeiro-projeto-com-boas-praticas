using System.Net;
using System.Net.Mail;

namespace Recomendador.Playground
{
    public class Gmail
    {
        public const string HOST = "smtp.gmail.com";

        public const int PORT = 587;

        public ContaSmtp Conta { get; set; }

        public Gmail(ContaSmtp contaSmtp)
        {
            // TODO: Validação!

            this.Conta = contaSmtp;
        }

        public void EnviarArtigo(MensagemEmail mensagem)
        {
            var mailMessage = mensagem.ObterMailMessage();

            var smtpClient = this.ObterSmtpClient();

            mailMessage.From = new MailAddress(this.Conta.Email, this.Conta.Nome);

            smtpClient.Send(mailMessage);
        }

        public SmtpClient ObterSmtpClient()
        {
            return new SmtpClient(HOST, PORT)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(this.Conta.Email, this.Conta.Senha)
            };
        }
    }
}
