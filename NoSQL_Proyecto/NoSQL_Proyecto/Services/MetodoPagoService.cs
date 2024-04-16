using NoSQL_Proyecto.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NoSQL_Proyecto.Services
{
    public class MetodoPagoService
    {
        private readonly IMongoDatabase _database;

        public MetodoPagoService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<Metodo_Pago> GetCollection(string collectionName)
        {
            return _database.GetCollection<Metodo_Pago>(collectionName);
        }

        public async Task<List<Metodo_Pago>> GetAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Metodo_Pago?> GetAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string collectionName, Metodo_Pago newMetodoPago)
        {
            var collection = GetCollection(collectionName);
            await collection.InsertOneAsync(newMetodoPago);
        }

        public async Task UpdateAsync(string collectionName, ObjectId id, Metodo_Pago updatedMetodoPago)
        {
            var collection = GetCollection(collectionName);
            await collection.ReplaceOneAsync(x => x.Id == id, updatedMetodoPago);
        }

        public async Task RemoveAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            await collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
