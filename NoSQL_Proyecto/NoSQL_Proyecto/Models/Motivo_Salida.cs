using Humanizer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace NoSQL_Proyecto.Models
{
    public class Motivo_Salida  
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public ObjectId Id { get; set; }
        public string Tipo_Salida { get; set; }
    }

    public class Motivo_SalidaViewModel
    {
        public string Tipo_Salida { get; set; }
    }
}
