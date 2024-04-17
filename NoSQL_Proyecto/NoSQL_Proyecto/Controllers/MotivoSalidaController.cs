using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class MotivoSalidaController : Controller
    {
        private readonly MotivoSalidaService _motivoSalidaService;

        public MotivoSalidaController(MotivoSalidaService motivoSalidaService)
        {
            _motivoSalidaService = motivoSalidaService;
        }
        public async Task<IActionResult> Motivos()
        {
            var motivos = await _motivoSalidaService.GetAsync("Motivo_Salida");

            if (motivos != null)
            {
                return View(motivos);
            }
            else
            {
                return Problem("La lista de motivos es nula.");
            }
        }


        public async Task<IActionResult> EditMotivo(ObjectId id)
        {

            var motivos = await _motivoSalidaService.GetAsync("Motivo_Salida", id);

            if (motivos != null)
            {
                return View(motivos);
            }


            return RedirectToAction("Motivos");
        }

        [HttpPost]
        public async Task<IActionResult> EditMotivo(Motivo_Salida model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el proveedor en la base de datos
                    await _motivoSalidaService.UpdateAsync("Motivo_Salida", model.Id, model);

                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el motivo: " + ex.Message);
                }
            }

            // Obtener nuevamente la lista de proveedores después de la actualización
            var motivos = await _motivoSalidaService.GetAsync("Motivo_Salida");

            // Devolver la vista con la lista de proveedores
            return View("Motivos", motivos);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteMotivo(ObjectId id)
        {

            await _motivoSalidaService.RemoveAsync("Motivo_Salida", id);

            var metodos = await _motivoSalidaService.GetAsync("Motivo_Salida");

            return View("Motivos", metodos);
        }


        public IActionResult CreateMotivo()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMotivo(Motivo_Salida motivos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _motivoSalidaService.CreateAsync("Motivo_Salida", motivos);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "Error al crear el motivo: " + ex.Message);
                }

            }

            var list = await _motivoSalidaService.GetAsync("Motivo_Salida");

            // Devolver la vista con la lista de proveedores
            return View("Motivos", list);
        }
    }
}
