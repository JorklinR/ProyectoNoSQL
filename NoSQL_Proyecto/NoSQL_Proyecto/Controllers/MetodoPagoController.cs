using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class MetodoPagoController : Controller
    {
        private readonly MetodoPagoService _metodoPagoService;

        public MetodoPagoController(MetodoPagoService metodoPagoService)
        {
            _metodoPagoService = metodoPagoService;
        }
        public async Task<IActionResult> Metodos()
        {
            var metodos = await _metodoPagoService.GetAsync("Metodo_Pago");

            if (metodos != null)
            {
                return View(metodos);
            }
            else
            {
                return Problem("La lista de metodos es nula.");
            }
        }

        public async Task<IActionResult> EditMetodo(ObjectId id)
        {

            var metodos = await _metodoPagoService.GetAsync("Metodo_Pago", id);

            if (metodos != null)
            {
                return View(metodos);
            }


            return RedirectToAction("Metodos");
        }


        [HttpPost]
        public async Task<IActionResult> EditMetodo(Metodo_Pago model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el proveedor en la base de datos
                    await _metodoPagoService.UpdateAsync("Metodo_Pago", model.Id, model);

                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el metodo: " + ex.Message);
                }
            }

            // Obtener nuevamente la lista de proveedores después de la actualización
            var metodos = await _metodoPagoService.GetAsync("Metodo_Pago");

            // Devolver la vista con la lista de proveedores
            return View("Metodos", metodos);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMetodo(ObjectId id)
        {

            await _metodoPagoService.RemoveAsync("Metodo_Pago", id);

            var articulos = await _metodoPagoService.GetAsync("Metodo_Pago");

            return View("Metodos", articulos);
        }


        public IActionResult CreateMetodo()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMetodo(Metodo_Pago metodos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _metodoPagoService.CreateAsync("Metodo_Pago", metodos);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "Error al crear el metodo: " + ex.Message);
                }

            }

            var list = await _metodoPagoService.GetAsync("Metodo_Pago");

            // Devolver la vista con la lista de proveedores
            return View("Metodos", list);
        }
    }
}
