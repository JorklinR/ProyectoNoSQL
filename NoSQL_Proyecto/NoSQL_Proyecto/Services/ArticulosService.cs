using NoSQL_Proyecto.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NoSQL_Proyecto.Services
{
    public class ArticulosService
    {
        private readonly IMongoDatabase _database;

        public ArticulosService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<Articulos> GetCollection(string collectionName)
        {
            return _database.GetCollection<Articulos>(collectionName);
        }

        public async Task<List<Articulos>> GetAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Articulos?> GetAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string collectionName, Articulos newArticulo)
        {
            var collection = GetCollection(collectionName);
            await collection.InsertOneAsync(newArticulo);
        }

        public async Task UpdateAsync(string collectionName, ObjectId id, Articulos updatedArticulo)
        {
            var collection = GetCollection(collectionName);
            await collection.ReplaceOneAsync(x => x.Id == id, updatedArticulo);
        }

        public async Task RemoveAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            await collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
