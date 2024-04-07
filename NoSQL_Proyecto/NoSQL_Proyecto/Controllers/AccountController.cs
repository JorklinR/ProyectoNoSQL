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
