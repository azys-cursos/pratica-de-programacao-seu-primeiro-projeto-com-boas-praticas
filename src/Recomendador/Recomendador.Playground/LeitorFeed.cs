using System;
using System.Collections.Generic;
using System.Linq;
using TNX.RssReader;

namespace Recomendador.Playground
{
    public class LeitorFeed
    {
        public Blog Blog { get; set; }

        public LeitorFeed(Blog blog)
        {
            this.Blog = blog;
        }

        public Artigo ObterArtigoAleatorio(IEnumerable<Recomendacao> recomendacoesFeitas)
        {
            var posts = this.ObterPosts(this.Blog.Feed);

            var artigos = posts
                            .Where(p => p.Link.Contains(this.Blog.TermoFiltragem))
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
