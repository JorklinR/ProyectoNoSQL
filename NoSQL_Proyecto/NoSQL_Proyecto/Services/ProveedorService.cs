using NoSQL_Proyecto.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NoSQL_Proyecto.Services
{
    public class ProveedorService
    {
        private readonly IMongoDatabase _database;

        public ProveedorService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<Proveedor> GetCollection(string collectionName)
        {
            return _database.GetCollection<Proveedor>(collectionName);
        }

        public async Task<List<Proveedor>> GetAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Proveedor?> GetAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string collectionName, Proveedor newProveedor)
        {
            var collection = GetCollection(collectionName);
            await collection.InsertOneAsync(newProveedor);
        }

        public async Task UpdateAsync(string collectionName, ObjectId id, Proveedor updatedProveedor)
        {
            var collection = GetCollection(collectionName);
            await collection.ReplaceOneAsync(x => x.Id == id, updatedProveedor);
        }

        public async Task RemoveAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            await collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Proveedor>> GetRecentProvider(string collectionName)
        {
            var collection = GetCollection(collectionName);

            // Ordenar los documentos por fecha de creación de forma descendente
            var sortedDocuments = await collection.Find(_ => true)
                                                 .SortByDescending(p => p.id_Tipo_Proveedor)
                                                 .ToListAsync();

            // Tomar los primeros 4 documentos (proveedores más recientes)
            var recentProviders = sortedDocuments.Take(4).ToList();

            return recentProviders;
        }
    }
}
