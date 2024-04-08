using Microsoft.AspNetCore.Mvc;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MongoDB.Bson;

namespace NoSQL_Proyecto.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public AccountController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Profile()
        {

            if (Request.Cookies.TryGetValue("Username", out string username))
            {
                var usuario = await _usuarioService.GetUserByUsername(username);

                if (usuario != null)
                {
                    return View(usuario);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Profile(Usuarios model, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioActual = await _usuarioService.GetAsync("Usuarios", model.Id);

                    usuarioActual.Nombre = string.IsNullOrEmpty(model.Nombre) ? usuarioActual.Nombre : model.Nombre;
                    usuarioActual.Mail = string.IsNullOrEmpty(model.Mail) ? usuarioActual.Mail : model.Mail;
                    usuarioActual.Phone = model.Phone == 0 ? usuarioActual.Phone : model.Phone;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Leer los datos de la imagen y convertirlos a un arreglo de bytes
                        using (var memoryStream = new MemoryStream())
                        {
                            await imageFile.CopyToAsync(memoryStream);
                            usuarioActual.Image = memoryStream.ToArray();
                        }
                    }

                    // Actualizar el usuario en la base de datos
                    await _usuarioService.UpdateAsync("Usuarios", usuarioActual.Id, usuarioActual);

                    ViewBag.Success = true;

                 
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el usuario: " + ex.Message);
                }
            }

            // Si hay errores de validación, ocurrirá esto
            return View("Profile", model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(ObjectId id)
        {
            try
            {
                var usuarioActual = await _usuarioService.GetAsync("Usuarios", id);

                // Eliminar la imagen del usuario
                usuarioActual.Image = null;

                // Actualizar el usuario en la base de datos
                await _usuarioService.UpdateAsync("Usuarios", usuarioActual.Id, usuarioActual);

                ViewBag.Success = true;

                // Redireccionar al usuario de vuelta a la vista de perfil
                return RedirectToAction("Profile", new { id = usuarioActual.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al eliminar la imagen del usuario: " + ex.Message);
                // Si hay errores de validación, ocurrirá esto
                return View("Profile");
            }
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
  
            if (ModelState.IsValid)
            {
         
                var usuario = await _usuarioService.GetUserByUsernameAndPassword(model.Username, model.Password);

            
                if (usuario != null)
                {
                 
                    Response.Cookies.Append("Username", usuario.Username);

               
                    return RedirectToAction("Index", "Home");
                }
                else
                {
             
                    ModelState.AddModelError(string.Empty, "Credenciales inválidas");
                }
            }


            return View("Login", model);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Verificar si el modelo es válido
            if (ModelState.IsValid)
            {
                // Verificar si el usuario ya existe
                var existingUser = await _usuarioService.GetUserByUsername(model.Username);
                var existingMail = await _usuarioService.GetUserByMail(model.Mail);
                if (existingUser != null || existingMail != null)
                {
                    // Si el usuario ya existe, configurar un mensaje de error en ViewBag
                    ViewBag.Success = false;
                    return View("Register", model);
                }

                // Crear un nuevo usuario
                var nuevoUsuario = new Usuarios
                {
                    Username = model.Username,
                    Password = model.Password,
                    Mail = model.Mail,
                    Fecha_Creacion = DateTime.UtcNow,
                    id_Tipo_Usuario = ObjectId.Parse("660dc3e881c5953b240d5f51"),
                    Active = true,

                    // Asigna otros campos de usuario según sea necesario
                };

                // Intenta crear el usuario
                try
                {
                    await _usuarioService.CreateAsync("Usuarios", nuevoUsuario);

                    // Si el usuario se crea con éxito, configurar un mensaje de éxito en ViewBag
                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al registrar el usuario: " + ex.Message);
                }
            }

            // Si hay errores de validación o si el usuario no se creó correctamente, volver a mostrar el formulario de registro
            return View("Register", model);
        }

        public IActionResult Logout()
        {

            // Eliminar la cookie "Username"
            Response.Cookies.Delete("Username");

            return RedirectToAction("Login", "Account");
        }

    }

}
