using System;
using System.Collections.Generic;
using System.Linq;
using Recomendador.Dominio;
using TNX.RssReader;

namespace Recomendador.Infra.Feeds
{
    public class LeitorFeed : ILeitorFeed
    {
        public Artigo ObterArtigoAleatorio(Blog blog, IEnumerable<Recomendacao> recomendacoesFeitas)
        {
            var posts = this.ObterPosts(blog.Feed);

            var artigos = posts
                            .Where(p => p.Link.Contains(blog.TermoFiltragem))
                            .Where(p => !recomendacoesFeitas.Any(r => r.Artigo == p.Link))
                            .ToList();

            var artigoSorteado = this.SortearArtigo(artigos);

            return new Artigo(artigoSorteado.Title, artigoSorteado.Link);
        }

        public IEnumerable<RssItem> ObterPosts(string url)
        {
            var feed = RssHelper.ReadFeed(url);

            return feed.Items;
        }

        public RssItem SortearArtigo(IList<RssItem> artigos)
        {
            var random = new Random();
            var numeroAleatorio = random.Next(0, artigos.Count());

            return  artigos[numeroAleatorio];
        }
    }
}
