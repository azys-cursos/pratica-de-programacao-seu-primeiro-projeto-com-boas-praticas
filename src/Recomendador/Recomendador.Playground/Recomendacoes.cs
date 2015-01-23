using System;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Recomendador.Playground
{
    public class Recomendacoes : IRecomendacoes // Possilitar a expensão e evitar alterações.
    {
        protected virtual MongoCollection<Recomendacao> Collection { get; set; }

        public Recomendacoes(MongoDatabase mongo) // Informar para o mundo o que é essencial para a classe funcionar...
        {
            // Reponsável por garantir o estado válido.

            if (mongo == null)
                throw new ArgumentNullException("mongo");

            this.Collection = mongo.GetCollection<Recomendacao>("Recomendacoes");
        }

        public virtual bool HaRecomendacoes(Usuario usuario, DateTime data)
        {
            return Collection
                        .AsQueryable()
                        .Any(r => r.Usuario == usuario.Email && r.Data == data);
        }

        public virtual IQueryable<Recomendacao> ObterTodas()
        {
            return Collection.AsQueryable();
        }

        public virtual void Registrar(Recomendacao recomendacao)
        {
            Collection.Insert(recomendacao);
        }
    }
}
