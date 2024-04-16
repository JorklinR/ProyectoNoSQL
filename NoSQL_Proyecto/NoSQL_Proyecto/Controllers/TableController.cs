using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class TableController : Controller
    {
        private readonly ArticulosService _articulosService;
        private readonly UsuarioService _usuarioService;
     
        private readonly MetodoPagoService _metodoPagoService;
        private readonly MotivoSalidaService _metodoSalidaService;
        private readonly ProveedorService _proveedorService;
      
        private readonly TipoArticulosService _tipoArticulosService;
        private readonly TipoProveedoresService _tipoProveedoresService;
        private readonly TipoUsuariosService _tipoUsuariosService;

        public TableController(ArticulosService articulosService, UsuarioService usuarioService, MetodoPagoService metodoPagoService, MotivoSalidaService metodoSalidaService,
               ProveedorService proveedorService, TipoArticulosService tipoArticulosService, TipoProveedoresService tipoProveedoresService, TipoUsuariosService tipoUsuariosService)
        {
            _articulosService = articulosService;
            _usuarioService = usuarioService;
            
            _metodoPagoService = metodoPagoService;
            _metodoSalidaService = metodoSalidaService;
            _proveedorService = proveedorService;
            
            _tipoArticulosService = tipoArticulosService;
            _tipoProveedoresService = tipoProveedoresService;
            _tipoUsuariosService = tipoUsuariosService;

        }

        public IActionResult Tables()
        {
            //Espacio para las variables
            var articulos = _articulosService.GetAsync("Articulos").Result;

            // Espacio para las listas
            List<ArticuloViewModel> articuloViewModels = new List<ArticuloViewModel>();

            // Espacio para los ForEach
            foreach (var articulo in articulos)
            {
                // Crear una nueva instancia de ArticuloViewModel y asignar precio y descripción
                var articuloViewModel = new ArticuloViewModel
                {
                    Precio = articulo.Precio,
                    Descripcion = articulo.Articulo
                };

                // Agregar el objeto ArticuloViewModel a la lista
                articuloViewModels.Add(articuloViewModel);
            }
            // Pasar la lista  a la vista usando ViewBag
            ViewBag.Articulos = articuloViewModels;

            return View();

        }

        /*
         ESPACIO PARA PROVEEDOR
         
         */
        public IActionResult Proveedor()
        {
            //Espacio para las variables
            var proveedores = _proveedorService.GetAsync("Proveedor").Result;

            // Espacio para las listas
            List<ProveedorViewModel> proveedorViewModels = new List<ProveedorViewModel>();

            // Espacio para los ForEach
            foreach (var proveedor in proveedores)
            {
                
                var proveedorViewModel = new ProveedorViewModel
                {
                     Id = proveedor.Id,
                     Nombre = proveedor.Nombre,
                     id_Tipo_Proveedor = proveedor.id_Tipo_Proveedor,
                     Direccion = proveedor.Direccion,
                     Mail = proveedor.Mail,
                     Phone = proveedor.Phone,
                    Active = proveedor.Active
    };

                // Agregar el objeto ArticuloViewModel a la lista
                proveedorViewModels.Add(proveedorViewModel);
            }
            // Pasar la lista  a la vista usando ViewBag
            ViewBag.Proveedores = proveedorViewModels;

            return View();

        }

        public async Task<IActionResult> EditProveedor(ObjectId id)
        {

                var proveedor = await _proveedorService.GetAsync("Proveedor",  id);

                if (proveedor != null)
                {
                    return View(proveedor);
                }
            

            return RedirectToAction("Proveedor", "Table");
        }

        [HttpPost]
        public async Task<IActionResult> EditProveedor(ProveedorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var proveedorActual = await _proveedorService.GetAsync("Proveedor", model.Id);

                    proveedorActual.Nombre = string.IsNullOrEmpty(model.Nombre) ? proveedorActual.Nombre : model.Nombre;
                    proveedorActual.Direccion = string.IsNullOrEmpty(model.Direccion) ? proveedorActual.Direccion : model.Nombre;
                    proveedorActual.id_Tipo_Proveedor = model.id_Tipo_Proveedor != null ? model.id_Tipo_Proveedor : proveedorActual.id_Tipo_Proveedor;
                    proveedorActual.Mail = string.IsNullOrEmpty(model.Mail) ? proveedorActual.Mail : model.Mail;
                    proveedorActual.Phone = model.Phone == 0 ? proveedorActual.Phone : model.Phone;
                    proveedorActual.Active = model.Active == false ? proveedorActual.Active : model.Active;


                    // Actualizar el usuario en la base de datos
                    await _proveedorService.UpdateAsync("Proveedor", proveedorActual.Id, proveedorActual);

                    ViewBag.Success = true;


                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el proveedor: " + ex.Message);
                }
            }

            // Si hay errores de validación, ocurrirá esto
            return View("Proveedor", model);
        }

        public IActionResult DeleteProveedor() { //Es llamado en la vista de edit proveedor para eliminarlo y actualizar la coleccion.

            return View();
        }

        /*
         * FIN DE
         * ESPACIO PARA PROVEEDOR

        */




        /*
           ESPACIO PARA METODO_PAGO
         */




        /*
         FIN PARA METODO_PAGO
         */


        /*
           ESPACIO PARA MOTIVO_SALIDA
         */




        /*
         FIN PARA MOTIVO_SALIDA
         */

        /*
           ESPACIO PARA TIPO_ARTICULOS
         */




        /*
         FIN PARA TIPO_ARTICULOS
         */


        /*
         ESPACIO PARA TIPO_PROVEEDORES
       */




        /*
         FIN PARA TIPO_PROVEEDORES
         */


        /*
         ESPACIO PARA TIPO_USUARIOS
         */




        /*
         FIN PARA TIPO_USUARIOS
         */



    }
}
