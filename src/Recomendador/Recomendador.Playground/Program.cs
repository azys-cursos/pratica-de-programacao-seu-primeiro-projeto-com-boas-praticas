using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using TNX.RssReader;

namespace Recomendador.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongodb = MongoDb.GetDatabase();

            var recomendacoes = new Recomendacoes(mongodb);

            var usuario = "denisferrari@azys.com.br";
            var hoje = DateTime.Today;

            Console.WriteLine("Há recomendações para esse usuário hoje?");
            Console.WriteLine("");

            var haRecomendacoes = recomendacoes.HaRecomendacoes(usuario, hoje);

            Console.WriteLine(haRecomendacoes);

            Console.ReadLine();

            if (haRecomendacoes)
                return;

            Console.WriteLine("Recomendações feitas até o momento!");
            Console.WriteLine("");

            var recomendacoesFeitas = recomendacoes.ObterTodas();

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

            var servicoEmail = new Gmail();

            servicoEmail.EnviarArtigo(usuario, artigoSelecionado.Title, artigoSelecionado.Link);

            Console.WriteLine("E-mail Enviado!");

            Console.ReadLine();

            // Como salvar as recomendações em MongoDB?

            var recomendacao = new Recomendacao()
            {
                Usuario = usuario,
                Artigo = artigoSelecionado.Link,
                Data = hoje
            };

            recomendacoes.Registrar(recomendacao);

            Console.WriteLine("Recomendação Registrada!");
            Console.WriteLine("");
            Console.ReadLine();
        }
    }
}
