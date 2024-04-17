using NoSQL_Proyecto.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NoSQL_Proyecto.Services
{
    public class TipoArticulosService
    {
        private readonly IMongoDatabase _database;

        public TipoArticulosService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<Tipo_Articulos> GetCollection(string collectionName)
        {
            return _database.GetCollection<Tipo_Articulos>(collectionName);
        }

        public async Task<List<Tipo_Articulos>> GetAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Tipo_Articulos?> GetAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string collectionName, Tipo_Articulos newTipoArticulo)
        {
            var collection = GetCollection(collectionName);
            await collection.InsertOneAsync(newTipoArticulo);
        }

        public async Task UpdateAsync(string collectionName, ObjectId id, Tipo_Articulos updatedTipoArticulo)
        {
            var collection = GetCollection(collectionName);
            await collection.ReplaceOneAsync(x => x.Id == id, updatedTipoArticulo);
        }

        public async Task RemoveAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            await collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Tipo_Articulos>> GetRecentTipo(string collectionName)
        {
            var collection = GetCollection(collectionName);

            // Ordenar los documentos por fecha de creación de forma descendente
            var sortedDocuments = await collection.Find(_ => true)
                                                 .SortByDescending(p => p.Id)
                                                 .ToListAsync();

            // Tomar los primeros 4 documentos (proveedores más recientes)
            var recentTipo = sortedDocuments.Take(4).ToList();

            return recentTipo;
        }
    }
}
