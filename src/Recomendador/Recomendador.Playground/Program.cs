using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TNX.RssReader;

namespace Recomendador.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "mongodb://r2d2:dfs465789ds47815@ds031541.mongolab.com:31541/recomendador";
            var database = new MongoUrl(connectionString).DatabaseName;
            var mongodb = new MongoClient(connectionString)
                .GetServer()
                .GetDatabase(database);

            BsonClassMap.RegisterClassMap<Recomendacao>(cm =>
            {
                cm.AutoMap();
                cm.IdMemberMap.SetIdGenerator(CombGuidGenerator.Instance);
            });

            var recomendacoes = mongodb.GetCollection<Recomendacao>("Recomendacoes");

            var usuario = "denisferrari@azys.com.br";
            var hoje = DateTime.Today;

            Console.WriteLine("Há recomendações para esse usuário hoje?");
            Console.WriteLine("");

            var haRecomendacoes = recomendacoes
                                    .AsQueryable()
                                    .Any(r => r.Usuario == usuario && r.Data == hoje);

            Console.WriteLine(haRecomendacoes);

            Console.ReadLine();

            if (haRecomendacoes)
                return;

            Console.WriteLine("Recomendações feitas até o momento!");
            Console.WriteLine("");

            var recomendacoesFeitas = recomendacoes.AsQueryable();

            foreach (var recomendacaoFeita in recomendacoesFeitas)
            {
                Console.WriteLine(recomendacaoFeita.Artigo);
            }

            Console.ReadLine();

            // Como obter os posts do Blog do Martin Fowler?
            var urlFeedBlogMartinFowler = "http://martinfowler.com/feed.atom";

            var feed = RssHelper.ReadFeed(urlFeedBlogMartinFowler);

            var posts = feed.Items;

            var artigos = posts
                            .Where(p => p.Link.Contains("articles") && !recomendacoesFeitas.Any(r => r.Artigo == p.Link))
                            .ToList();

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
            mailMessage.To.Add(usuario);

            // smtpClient.Send(mailMessage);

            Console.WriteLine("E-mail Enviado!");

            Console.ReadLine();

            // Como salvar as recomendações em MongoDB?

            var recomendacao = new Recomendacao()
            {
                Usuario = usuario,
                Artigo = artigoSelecionado.Link,
                Data = hoje
            };

            recomendacoes.Insert(recomendacao);

            Console.WriteLine("Recomendação Registrada!");
            Console.WriteLine("");
            Console.ReadLine();
        }

        public class Recomendacao
        {
            public Guid Id { get; set; }

            public string Usuario { get; set; }

            public string Artigo { get; set; }

            public DateTime Data { get; set; }
        }
    }
}
