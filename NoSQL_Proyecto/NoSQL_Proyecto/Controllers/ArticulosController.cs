using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly ArticulosService _articulosService;
        public ArticulosController(ArticulosService articulosService)
        {
            _articulosService = articulosService;
        }

        public async Task<IActionResult> Articulos()
        {
            var articulos = await _articulosService.GetAsync("Articulos");

            if (articulos != null)
            {
                return View(articulos);
            }
            else
            {
                return Problem("La lista de articulos es nula.");
            }
        }


        public async Task<IActionResult> EditArticulo(ObjectId id)
        {

            var articulo = await _articulosService.GetAsync("Articulos", id);

            if (articulo != null)
            {
                return View(articulo);
            }


            return RedirectToAction("Articulos", "Articulos");
        }

        [HttpPost]
        public async Task<IActionResult> EditArticulo(Articulos model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el proveedor en la base de datos
                    await _articulosService.UpdateAsync("Articulos", model.Id, model);

                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el articulo: " + ex.Message);
                }
            }

            // Obtener nuevamente la lista de proveedores después de la actualización
            var articulos = await _articulosService.GetAsync("Articulos");

            // Devolver la vista con la lista de proveedores
            return View("Articulos", articulos);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteArticulo(ObjectId id)
        {

            await _articulosService.RemoveAsync("Articulos", id);

            var articulos = await _articulosService.GetAsync("Articulos");

            return View("Articulos", articulos);
        }


        public IActionResult CreateArticulo()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticulo(Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _articulosService.CreateAsync("Articulos", articulos);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "Error al crear el articulo: " + ex.Message);
                }

            }

            var list = await _articulosService.GetAsync("Articulos");

            // Devolver la vista con la lista de proveedores
            return View("Articulos", list);
        }
    }
    
 }
