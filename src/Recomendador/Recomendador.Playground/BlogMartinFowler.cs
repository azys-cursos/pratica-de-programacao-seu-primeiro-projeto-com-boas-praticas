using System;
using System.Collections.Generic;
using System.Linq;
using TNX.RssReader;

namespace Recomendador.Playground
{
    public class BlogMartinFowler
    {


        public Artigo ObterArtigoAleatorio(IEnumerable<Recomendacao> recomendacoesFeitas)
        {
            var urlFeedBlogMartinFowler = "http://martinfowler.com/feed.atom";

            var feed = RssHelper.ReadFeed(urlFeedBlogMartinFowler);

            var posts = feed.Items;

            var artigos = posts
                            .Where(p => p.Link.Contains("articles") && !recomendacoesFeitas.Any(r => r.Artigo == p.Link))
                            .ToList();

            var random = new Random();
            var numeroAleatorio = random.Next(0, artigos.Count());
            var artigoSelecionado = artigos[numeroAleatorio];

            return new Artigo(artigoSelecionado.Title, artigoSelecionado.Link);
        }
    }
}
