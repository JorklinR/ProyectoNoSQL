using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class TipoArticuloController : Controller
    {
        private readonly TipoArticulosService _tipoArticuloService;

        public TipoArticuloController(TipoArticulosService tipoArticulosService)
        {
            _tipoArticuloService = tipoArticulosService;
        }

        public async Task<IActionResult> TipoArticulos()
        {
            var tipos = await _tipoArticuloService.GetAsync("Tipo_Articulos");

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

            var tipos = await _tipoArticuloService.GetAsync("Tipo_Articulos", id);

            if (tipos != null)
            {
                return View(tipos);
            }


            return RedirectToAction("TipoArticulos");
        }

        [HttpPost]
        public async Task<IActionResult> EditTipo(Tipo_Articulos model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el proveedor en la base de datos
                    await _tipoArticuloService.UpdateAsync("Tipo_Articulos", model.Id, model);

                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el tipo: " + ex.Message);
                }
            }

            // Obtener nuevamente la lista de proveedores después de la actualización
            var tipos = await _tipoArticuloService.GetAsync("Tipo_Articulos");

            // Devolver la vista con la lista de proveedores
            return View("TipoArticulos", tipos);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTipo(ObjectId id)
        {

            await _tipoArticuloService.RemoveAsync("Tipo_Articulos", id);

            var tipos = await _tipoArticuloService.GetAsync("Tipo_Articulos");

            return View("TipoArticulos", tipos);
        }


        public IActionResult CreateTipo()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipo(Tipo_Articulos tipos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tipoArticuloService.CreateAsync("Tipo_Articulos", tipos);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "Error al crear el tipo: " + ex.Message);
                }

            }

            var list = await _tipoArticuloService.GetAsync("Tipo_Articulos");

            // Devolver la vista con la lista de proveedores
            return View("TipoArticulos", list);
        }
    }

}
