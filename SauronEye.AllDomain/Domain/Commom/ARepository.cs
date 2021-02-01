using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Commom
{
    public abstract class ARepository 
    {
        private string username = "";
        private string password = "";
        private string databaseName = "";

        protected ARepository(IConfiguration config)
        {
            username = "admin";// config.GetSection("MongoDB:Username").Value;
            password = "admin";// config.GetSection("MongoDB:Password").Value;
            databaseName = config.GetSection("MongoDB:Password").Value;
        }


        private IMongoClient GetClient()
        {
            if (this.username == "")
                return new MongoClient($"mongodb://localhost:27017/krossmasters");
            else
                return new MongoClient($"mongodb://{username}:{password}@cluster0-shard-00-00.eiquk.mongodb.net:27017,cluster0-shard-00-01.eiquk.mongodb.net:27017,cluster0-shard-00-02.eiquk.mongodb.net:27017/krossmasters?ssl=true&replicaSet=atlas-55e1fl-shard-0&authSource=admin&retryWrites=true&w=majority");

        }
        protected IMongoDatabase GetDB()
        {
            var client = GetClient();
            databaseName = string.IsNullOrEmpty(databaseName) ? "krossmasters" : databaseName;
            var database = client.GetDatabase(databaseName);
            return database;
        }

        protected async Task<bool> CheckCollection(IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collectionCursor = await database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
            return await collectionCursor.AnyAsync();
        }
    }
}
