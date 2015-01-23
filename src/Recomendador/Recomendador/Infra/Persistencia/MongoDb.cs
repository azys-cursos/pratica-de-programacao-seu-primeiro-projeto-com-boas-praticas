using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using Recomendador.Dominio;

namespace Recomendador.Infra.Persistencia
{
    public class MongoDb
    {
        static MongoDb()
        {
            BsonClassMap.RegisterClassMap<Recomendacao>(cm =>
            {
                cm.AutoMap();
                cm.IdMemberMap.SetIdGenerator(CombGuidGenerator.Instance);
            });
        }

        public static MongoDatabase GetDatabase()
        {
            var connectionString = "mongodb://r2d2:dfs465789ds47815@ds031541.mongolab.com:31541/recomendador";
            var database = new MongoUrl(connectionString).DatabaseName;
            var mongodb = new MongoClient(connectionString)
                .GetServer()
                .GetDatabase(database);

            return mongodb;
        }
    }
}
