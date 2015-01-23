using System.Collections.Generic;

namespace Recomendador.Dominio
{
    public interface ILeitorFeed
    {
        Artigo ObterArtigoAleatorio(Blog blog, IEnumerable<Recomendacao> recomendacoesFeitas);
    }
}