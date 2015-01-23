using System;
using System.Linq;

namespace Recomendador.Dominio
{
    public interface IRecomendacoes
    {
        bool HaRecomendacoes(Usuario usuario, DateTime data);

        IQueryable<Recomendacao> ObterTodas();

        void Registrar(Recomendacao recomendacao);
    }
}