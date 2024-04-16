using Humanizer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace NoSQL_Proyecto.Models
{
    public class Tipo_Proveedores
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Descripcion_Proveedor { get; set; }

    }

    public class Tipo_ProveedoresViewModel
    {
        public string Descripcion_Proveedor { get; set; }
    }
}
