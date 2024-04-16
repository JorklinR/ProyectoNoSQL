
using Humanizer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace NoSQL_Proyecto.Models

    {

    public class Ingresos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public ObjectId id_Articulo { get; set; }
        public int Cantidad_Ingresada { get; set; }
        public double Precio_Compra {  get; set; }
        public ObjectId id_Proveedor { get; set; }

    }

    public class IngresosViewModel
    {
        public int Cantidad_Ingresada { get; set; }
        public double Precio_Compra { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public ObjectId id_Articulo { get; set; }
       public ObjectId id_Proveedor { get; set; }
    }
}
