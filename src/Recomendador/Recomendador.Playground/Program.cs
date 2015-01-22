using System;
using System.Collections.Generic;
using System.Linq;
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
            // Como salvar as recomendações em MongoDB?


        }
    }
}
