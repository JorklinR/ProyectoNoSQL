using Microsoft.AspNetCore.Mvc;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class TableController : Controller
    {
        private readonly ArticulosService _articulosService;
        private readonly UsuarioService _usuarioService;

        public TableController(ArticulosService articulosService, UsuarioService usuarioService)
        {
            _articulosService = articulosService;
            _usuarioService = usuarioService;
        }

        public IActionResult Tables()
        {
            var articulos = _articulosService.GetAsync("Articulos").Result;

            // Crear una lista de ArticuloViewModel para almacenar precios y descripciones
            List<ArticuloViewModel> articuloViewModels = new List<ArticuloViewModel>();

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

            // Pasar la lista de ArticuloViewModel a la vista usando ViewBag
            ViewBag.Articulos = articuloViewModels;

            return View();
        }


    }
}
