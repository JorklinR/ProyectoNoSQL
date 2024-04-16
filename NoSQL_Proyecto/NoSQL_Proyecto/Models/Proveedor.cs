using Humanizer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace NoSQL_Proyecto.Models
{
    public class Proveedor
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Nombre { get; set; }
        public ObjectId id_Tipo_Proveedor { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }
        public int Phone { get; set; }
        public Boolean Active { get; set; }



    }

    public class ProveedorViewModel
    {
        public ObjectId Id { get; set; }
        public string Nombre { get; set; }
        public ObjectId id_Tipo_Proveedor { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }
        public int Phone { get; set; }
        public Boolean Active { get; set; }

    }
}
