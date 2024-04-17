using NoSQL_Proyecto.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NoSQL_Proyecto.Services
{
    public class IngresosService
    {
        private readonly IMongoDatabase _database;

        public IngresosService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<Ingresos> GetCollection(string collectionName)
        {
            return _database.GetCollection<Ingresos>(collectionName);
        }

        public async Task<List<Ingresos>> GetAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Ingresos?> GetAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string collectionName, Ingresos newIngreso)
        {
            var collection = GetCollection(collectionName);
            await collection.InsertOneAsync(newIngreso);
        }

        public async Task UpdateAsync(string collectionName, ObjectId id, Ingresos updatedIngreso)
        {
            var collection = GetCollection(collectionName);
            await collection.ReplaceOneAsync(x => x.Id == id, updatedIngreso);
        }

        public async Task RemoveAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            await collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<int> GetTotalIngresos(string collectionName)
        {
            var collection = GetCollection(collectionName);
            var documentos = await collection.Find(_ => true).ToListAsync();

            // Sumar los valores de 'Cantidad_Ingresada' en todos los documentos
            int sumaTotal = documentos.Sum(d => d.Cantidad_Ingresada);

            return sumaTotal;
        }


    }
}
