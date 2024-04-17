using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public async Task<IActionResult> Usuarios()
        {
            var usuarios = await _usuarioService.GetAsync("Usuarios");

            if (usuarios != null)
            {
                return View(usuarios);
            }
            else
            {
                return Problem("La lista de usuarios es nula.");
            }
        }

        public async Task<IActionResult> EditUsuario(ObjectId id)
        {

            var usuario = await _usuarioService.GetAsync("Usuarios", id);

            if (usuario != null)
            {
                return View(usuario);
            }


            return RedirectToAction("Usuarios");
        }


        [HttpPost]
        public async Task<IActionResult> EditUsuario(Usuarios model, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioActual = await _usuarioService.GetAsync("Usuarios", model.Id);

                    usuarioActual.Nombre = string.IsNullOrEmpty(model.Nombre) ? usuarioActual.Nombre : model.Nombre;
                    usuarioActual.Mail = string.IsNullOrEmpty(model.Mail) ? usuarioActual.Mail : model.Mail;
                    usuarioActual.Phone = model.Phone == 0 ? usuarioActual.Phone : model.Phone;
                    usuarioActual.Active = model.Active;

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

            // Obtener nuevamente la lista de proveedores después de la actualización
            var usuarios = await _usuarioService.GetAsync("Usuarios");

            // Devolver la vista con la lista de proveedores
            return View("Usuarios", usuarios);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUsuario(ObjectId id)
        {

            await _usuarioService.RemoveAsync("Usuarios", id);

            var usuarios = await _usuarioService.GetAsync("Usuarios");

            return View("Usuarios", usuarios);
        }
    }
}
