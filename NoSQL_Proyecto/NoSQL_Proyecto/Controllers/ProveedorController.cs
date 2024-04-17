using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class ProveedorController : Controller
 
    {
        private readonly ProveedorService _proveedorService;

        public ProveedorController(ProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        /*
         ESPACIO PARA PROVEEDOR
         
         */
        public async Task<IActionResult> Proveedor()
        {
            var proveedores = await _proveedorService.GetAsync("Proveedor");

            if (proveedores != null)
            {
                return View(proveedores);
            }
            else
            {
                return Problem("La lista de proveedores es nula.");
            }
        }


        public async Task<IActionResult> EditProveedor(ObjectId id)
        {

            var proveedor = await _proveedorService.GetAsync("Proveedor", id);

            if (proveedor != null)
            {
                return View(proveedor);
            }


            return RedirectToAction("Proveedor", "Proveedor");
        }

        [HttpPost]
        public async Task<IActionResult> EditProveedor(Proveedor model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var proveedorActual = await _proveedorService.GetAsync("Proveedor", model.Id);

                    // Actualizar propiedades del proveedor actual
                    proveedorActual.Nombre = string.IsNullOrEmpty(model.Nombre) ? proveedorActual.Nombre : model.Nombre;
                    proveedorActual.Direccion = string.IsNullOrEmpty(model.Direccion) ? proveedorActual.Direccion : model.Direccion;
                    proveedorActual.id_Tipo_Proveedor = model.id_Tipo_Proveedor != null ? model.id_Tipo_Proveedor : proveedorActual.id_Tipo_Proveedor;
                    proveedorActual.Mail = string.IsNullOrEmpty(model.Mail) ? proveedorActual.Mail : model.Mail;
                    proveedorActual.Phone = model.Phone == 0 ? proveedorActual.Phone : model.Phone;
                    proveedorActual.Active = model.Active;

                    // Actualizar el proveedor en la base de datos
                    await _proveedorService.UpdateAsync("Proveedor", proveedorActual.Id, proveedorActual);

                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el proveedor: " + ex.Message);
                }
            }

            // Obtener nuevamente la lista de proveedores después de la actualización
            var proveedores = await _proveedorService.GetAsync("Proveedor");

            // Devolver la vista con la lista de proveedores
            return View("Proveedor", proveedores);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProveedor(ObjectId id)
        {

            await _proveedorService.RemoveAsync("Proveedor", id);

            var proveedores = await _proveedorService.GetAsync("Proveedor");

            return View("Proveedor", proveedores);
        }


        public IActionResult CreateProveedor()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProveedor(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _proveedorService.CreateAsync("Proveedor", proveedor);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "Error al crear el proveedor: " + ex.Message);
                }

            }

            var proveedores = await _proveedorService.GetAsync("Proveedor");

            // Devolver la vista con la lista de proveedores
            return View("Proveedor", proveedores);
        }

        /*
         * FIN DE
         * ESPACIO PARA PROVEEDOR

        */
    }
}
