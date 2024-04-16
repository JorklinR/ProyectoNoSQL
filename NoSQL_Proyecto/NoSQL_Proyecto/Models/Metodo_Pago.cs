using Humanizer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace NoSQL_Proyecto.Models
{
    public class Metodo_Pago
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public ObjectId Id { get; set; }
        public string Tipo_Pago { get; set; }
    }

    public class Metodo_PagoViewModel
    {
        public string Tipo_Pago { get; set; }
    }

}
