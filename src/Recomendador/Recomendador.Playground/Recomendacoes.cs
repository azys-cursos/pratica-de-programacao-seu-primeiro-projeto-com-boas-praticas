using System;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Recomendador.Playground
{
    public class Recomendacoes
    {
        protected MongoCollection<Recomendacao> Collection { get; set; }

        public Recomendacoes(MongoDatabase mongo)
        {


            this.Collection = mongo.GetCollection<Recomendacao>("Recomendacoes");
        }

        public bool HaRecomendacoes(Usuario usuario, DateTime data)
        {
            return Collection
                        .AsQueryable()
                        .Any(r => r.Usuario == usuario.Email && r.Data == data);
        }

        public IQueryable<Recomendacao> ObterTodas()
        {
            return Collection.AsQueryable();
        }

        public void Registrar(Recomendacao recomendacao)
        {
            Collection.Insert(recomendacao);
        }
    }
}
