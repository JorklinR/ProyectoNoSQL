using NoSQL_Proyecto.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NoSQL_Proyecto.Services
{
    public class SalidasService
    {
        private readonly IMongoDatabase _database;

        public SalidasService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<Salidas> GetCollection(string collectionName)
        {
            return _database.GetCollection<Salidas>(collectionName);
        }

        public async Task<List<Salidas>> GetAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Salidas?> GetAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string collectionName, Salidas newSalida)
        {
            var collection = GetCollection(collectionName);
            await collection.InsertOneAsync(newSalida);
        }

        public async Task UpdateAsync(string collectionName, ObjectId id, Salidas updatedSalida)
        {
            var collection = GetCollection(collectionName);
            await collection.ReplaceOneAsync(x => x.Id == id, updatedSalida);
        }

        public async Task RemoveAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            await collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
