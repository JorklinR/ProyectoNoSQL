using Humanizer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NoSQL_Proyecto.Models;
namespace NoSQL_Proyecto.Models
{
    public class Salidas
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public DateTime Fecha_Salida { get; set; }
        public ObjectId id_Articulo { get; set; }
        public int Cantidad_Vendida { get; set; }
        public double Precio_Venta { get; set; }
        public ObjectId Motivo_Salida { get; set; }
        public ObjectId Metodo_Pago { get; set; }
    }

    public class SalidasViewModel
    {
        public DateTime Fecha_Salida { get; set; }
        public int Cantidad_Vendida { get; set; }
        public ObjectId Metodo_Pago { get; set; }
        public ObjectId id_Articulo { get; set; }

        public double Precio_Venta { get; set; }

        public ObjectId Motivo_Salida { get; set; }
    }
}



           