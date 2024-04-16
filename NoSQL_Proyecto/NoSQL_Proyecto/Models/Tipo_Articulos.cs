using Humanizer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace NoSQL_Proyecto.Models
{
    public class Tipo_Articulos
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Tipo_Articulo { get; set; }

    }

    public class Tipo_ArticulosViewModel
    {
        public string Tipo_Articulo { get; set; }
    }
}
