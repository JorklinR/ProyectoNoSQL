
using Humanizer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NoSQL_Proyecto.Models

{
  
    public class Usuarios
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } 
        public string? Nombre { get; set; }
        public ObjectId id_Tipo_Usuario { get; set; } 
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Mail { get; set; }
        public int Phone { get; set; }
        public byte[]? Image { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public Boolean Active { get; set; }


    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }

    }

    public class RegisterViewModel
    {

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Mail { get; set; }

        public DateTime Fecha_Creacion { get; set; }

    }



}
