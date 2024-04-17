using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class TipoUsuarioController : Controller
    {
        private readonly TipoUsuariosService _tipoUsuarioService;

        public TipoUsuarioController(TipoUsuariosService tipoUsuariosService)
        {
            _tipoUsuarioService = tipoUsuariosService;
        }

            public async Task<IActionResult> TipoUsuarios()
            {
                var tipos = await _tipoUsuarioService.GetAsync("Tipo_Usuarios");

                if (tipos != null)
                {
                    return View(tipos);
                }
                else
                {
                    return Problem("La lista de tipos es nula.");
                }
            }

        public async Task<IActionResult> EditTipo(ObjectId id)
        {

            var tipos = await _tipoUsuarioService.GetAsync("Tipo_Usuarios", id);

            if (tipos != null)
            {
                return View(tipos);
            }


            return RedirectToAction("TipoUsuarios");
        }

        [HttpPost]
        public async Task<IActionResult> EditTipo(Tipo_Usuarios model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el proveedor en la base de datos
                    await _tipoUsuarioService.UpdateAsync("Tipo_Usuarios", model.Id, model);

                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el tipo: " + ex.Message);
                }
            }

            // Obtener nuevamente la lista de proveedores después de la actualización
            var tipos = await _tipoUsuarioService.GetAsync("Tipo_Usuarios");

            // Devolver la vista con la lista de proveedores
            return View("TipoUsuarios", tipos);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteTipo(ObjectId id)
        {

            await _tipoUsuarioService.RemoveAsync("Tipo_Usuarios", id);

            var tipos = await _tipoUsuarioService.GetAsync("Tipo_Usuarios");

            return View("TipoUsuarios", tipos);
        }

        public IActionResult CreateTipo()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipo(Tipo_Usuarios tipos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tipoUsuarioService.CreateAsync("Tipo_Usuarios", tipos);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "Error al crear el tipo: " + ex.Message);
                }

            }

            var list = await _tipoUsuarioService.GetAsync("Tipo_Usuarios");

            // Devolver la vista con la lista de proveedores
            return View("TipoUsuarios", list);
        }

    }
 }
