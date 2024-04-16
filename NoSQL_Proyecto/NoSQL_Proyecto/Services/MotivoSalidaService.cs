using NoSQL_Proyecto.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NoSQL_Proyecto.Services
{
    public class MotivoSalidaService
    {
        private readonly IMongoDatabase _database;

        public MotivoSalidaService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<Motivo_Salida> GetCollection(string collectionName)
        {
            return _database.GetCollection<Motivo_Salida>(collectionName);
        }

        public async Task<List<Motivo_Salida>> GetAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Motivo_Salida?> GetAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string collectionName, Motivo_Salida newMetodoSalida)
        {
            var collection = GetCollection(collectionName);
            await collection.InsertOneAsync(newMetodoSalida);
        }

        public async Task UpdateAsync(string collectionName, ObjectId id, Motivo_Salida updatedMetodoSalida)
        {
            var collection = GetCollection(collectionName);
            await collection.ReplaceOneAsync(x => x.Id == id, updatedMetodoSalida);
        }

        public async Task RemoveAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            await collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
