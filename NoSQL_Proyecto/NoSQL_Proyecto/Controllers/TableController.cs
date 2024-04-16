using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class TableController : Controller
    {
        private readonly ArticulosService _articulosService;
        private readonly UsuarioService _usuarioService;
        private readonly IngresosService _ingresosService;
        private readonly MetodoPagoService _metodoPagoService;
        private readonly MotivoSalidaService _metodoSalidaService;
        private readonly ProveedorService _proveedorService;
        private readonly SalidasService _salidaService;
        private readonly TipoArticulosService _tipoArticulosService;
        private readonly TipoProveedoresService _tipoProveedoresService;
        private readonly TipoUsuariosService _tipoUsuariosService;

        public TableController(ArticulosService articulosService, UsuarioService usuarioService, IngresosService ingresosService, MetodoPagoService metodoPagoService, MotivoSalidaService metodoSalidaService,
               ProveedorService proveedorService, SalidasService salidaService, TipoArticulosService tipoArticulosService, TipoProveedoresService tipoProveedoresService, TipoUsuariosService tipoUsuariosService)
        {
            _articulosService = articulosService;
            _usuarioService = usuarioService;
            _ingresosService = ingresosService;
            _metodoPagoService = metodoPagoService;
            _metodoSalidaService = metodoSalidaService;
            _proveedorService = proveedorService;
            _salidaService = salidaService;
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

    }
}
