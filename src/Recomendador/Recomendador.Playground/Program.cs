using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TNX.RssReader;

namespace Recomendador.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            // Como obter os posts do Blog do Martin Fowler?
            var urlFeedBlogMartinFowler = "http://martinfowler.com/feed.atom";

            var feed = RssHelper.ReadFeed(urlFeedBlogMartinFowler);

            var posts = feed.Items;

            var artigos = posts.Where(p => p.Link.Contains("articles")).ToList();

            var random = new Random();
            var numeroAleatorio = random.Next(0, artigos.Count());
            var artigoSelecionado = artigos[numeroAleatorio];

            Console.WriteLine(artigoSelecionado.Title);

            Console.ReadLine();

            // Como eu envio um e-mail?
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
            mailMessage.Subject = artigoSelecionado.Title;
            mailMessage.Body = artigoSelecionado.Link;
            mailMessage.To.Add("denisferrari@azys.com.br");

            smtpClient.Send(mailMessage);

            Console.WriteLine("E-mail Enviado!");

            // Como salvar as recomendações em MongoDB?


        }
    }
}
