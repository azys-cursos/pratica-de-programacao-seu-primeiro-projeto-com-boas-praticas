using System;
using System.Linq;

namespace Recomendador.Playground
{
    public interface IRecomendacoes
    {
        bool HaRecomendacoes(Usuario usuario, DateTime data);

        IQueryable<Recomendacao> ObterTodas();

        void Registrar(Recomendacao recomendacao);
    }
}