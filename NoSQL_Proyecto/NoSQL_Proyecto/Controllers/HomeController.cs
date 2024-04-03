using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ArticulosService _articulosService;

        public HomeController(ILogger<HomeController> logger, ArticulosService articulosService)
        {
            _logger = logger;
            _articulosService = articulosService;
        }


        public async Task<IActionResult> Index()
        {
            // Nombre de la colección predeterminado
            string collectionName = "Articulos";

            // Obtener todos los artículos
            var articulos = await _articulosService.GetAsync(collectionName);

            // Calcular la suma de los valores de los artículos
            decimal suma = 0;
            foreach (var articulo in articulos)
            {
                suma += articulo.Unidad_Stock;
            }

            // Pasar la suma a la vista
            ViewBag.SumaArticulos = suma;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
