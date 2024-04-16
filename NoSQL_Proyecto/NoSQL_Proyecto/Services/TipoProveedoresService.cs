using NoSQL_Proyecto.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NoSQL_Proyecto.Services
{
    public class TipoProveedoresService
    {
        private readonly IMongoDatabase _database;

        public TipoProveedoresService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<Tipo_Proveedores> GetCollection(string collectionName)
        {
            return _database.GetCollection<Tipo_Proveedores>(collectionName);
        }

        public async Task<List<Tipo_Proveedores>> GetAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Tipo_Proveedores?> GetAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string collectionName, Tipo_Proveedores newTipoProveedor)
        {
            var collection = GetCollection(collectionName);
            await collection.InsertOneAsync(newTipoProveedor);
        }

        public async Task UpdateAsync(string collectionName, ObjectId id, Tipo_Proveedores updatedTipoProveedor)
        {
            var collection = GetCollection(collectionName);
            await collection.ReplaceOneAsync(x => x.Id == id, updatedTipoProveedor);
        }

        public async Task RemoveAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            await collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
