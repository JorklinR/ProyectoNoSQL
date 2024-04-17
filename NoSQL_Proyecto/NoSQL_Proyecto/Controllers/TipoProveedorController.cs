using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class TipoProveedorController : Controller
    {
        private readonly TipoProveedoresService _tipoProveedoresService;

        public TipoProveedorController(TipoProveedoresService tipoProveedoresService)
        {
            _tipoProveedoresService = tipoProveedoresService;
        }
        public async Task<IActionResult> TipoProveedores()
        {
            var tipos = await _tipoProveedoresService.GetAsync("Tipo_Proveedores");

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

            var tipos = await _tipoProveedoresService.GetAsync("Tipo_Proveedores", id);

            if (tipos != null)
            {
                return View(tipos);
            }


            return RedirectToAction("TipoProveedores");
        }


        [HttpPost]
        public async Task<IActionResult> EditTipo(Tipo_Proveedores model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el proveedor en la base de datos
                    await _tipoProveedoresService.UpdateAsync("Tipo_Proveedores", model.Id, model);

                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el tipo: " + ex.Message);
                }
            }

            // Obtener nuevamente la lista de proveedores después de la actualización
            var tipos = await _tipoProveedoresService.GetAsync("Tipo_Proveedores");

            // Devolver la vista con la lista de proveedores
            return View("TipoProveedores", tipos);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteTipo(ObjectId id)
        {

            await _tipoProveedoresService.RemoveAsync("Tipo_Proveedores", id);

            var tipos = await _tipoProveedoresService.GetAsync("Tipo_Proveedores");

            return View("TipoProveedores", tipos);
        }


        public IActionResult CreateTipo()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipo(Tipo_Proveedores tipos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tipoProveedoresService.CreateAsync("Tipo_Proveedores", tipos);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "Error al crear el tipo: " + ex.Message);
                }

            }

            var list = await _tipoProveedoresService.GetAsync("Tipo_Proveedores");

            // Devolver la vista con la lista de proveedores
            return View("TipoProveedores", list);
        }

    }
}
