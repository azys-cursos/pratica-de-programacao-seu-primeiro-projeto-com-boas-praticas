using System.Collections.Generic;

namespace Recomendador.Playground
{
    public interface ILeitorFeed
    {
        Artigo ObterArtigoAleatorio(Blog blog, IEnumerable<Recomendacao> recomendacoesFeitas);
    }
}