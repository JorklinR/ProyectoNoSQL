
using Humanizer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace NoSQL_Proyecto.Models

{

    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;


    }
    public class Articulos
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } 
        public string Articulo { get; set; }
        public ObjectId id_Tipo_Articulo { get; set; } 
        public DateTime Fecha_Ingreso { get; set; }
        public DateTime? Fecha_Salida { get; set; } 
        public bool On_Stock { get; set; }
        public int Unidad_Stock { get; set; }
        public double Precio { get; set; }


    }
}
