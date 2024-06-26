﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NoSQL_Proyecto.Models;

namespace NoSQL_Proyecto.Services
{
    public class UsuarioService
    {

        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Usuarios> _usuariosCollection;

        public UsuarioService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _usuariosCollection = _database.GetCollection<Usuarios>("Usuarios");
        }

        public IMongoCollection<Usuarios> GetCollection(string collectionName)
        {
            return _database.GetCollection<Usuarios>(collectionName);
        }

        public async Task<List<Usuarios>> GetAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }


        public async Task<Usuarios?> GetAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string collectionName, Usuarios newUsuario)
        {
            var collection = GetCollection(collectionName);
            await collection.InsertOneAsync(newUsuario);
        }

        public async Task UpdateAsync(string collectionName, ObjectId id, Usuarios updatedUsuario)
        {
            var collection = GetCollection(collectionName);
            await collection.ReplaceOneAsync(x => x.Id == id, updatedUsuario);
        }

        public async Task RemoveAsync(string collectionName, ObjectId id)
        {
            var collection = GetCollection(collectionName);
            await collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<Usuarios> GetUserByUsernameAndPassword(string username, string password)
        {
            return await _usuariosCollection.Find(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        }

        public async Task<Usuarios> GetUserByUsername(string username)
        {
            return await _usuariosCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
        }
         public async Task<Usuarios> GetUserByMail(string Mail)
        {
            return await _usuariosCollection.Find(u => u.Mail == Mail).FirstOrDefaultAsync();
        }
        public int GetTotalUsuariosActivos()
        {
            var totalUsuariosActivos = _usuariosCollection.AsQueryable()
                .Where(u => u.Active)
                .Count();

            return totalUsuariosActivos;
        }

        public bool UsuarioTieneRol(string nombreUsuario)
        {
            // Construye el filtro para buscar el usuario por nombre de usuario
            var filter = Builders<Usuarios>.Filter.Eq(u => u.Username, nombreUsuario);

            // Realiza la consulta para encontrar el usuario que coincide con el nombre de usuario
            var usuario = _usuariosCollection.Find(filter).FirstOrDefault();

            // Si el usuario existe, verifica si tiene el rol deseado
            // Aquí asumimos que el id del tipo de usuario es un ObjectId
            var tipoUsuarioObjectId = new ObjectId("660dc3e881c5953b240d5f50");
            return usuario != null && usuario.id_Tipo_Usuario == tipoUsuarioObjectId;
        }

    }

}

