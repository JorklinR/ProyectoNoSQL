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
        private readonly UsuarioService _usuarioService;

        public HomeController(ILogger<HomeController> logger, ArticulosService articulosService, UsuarioService usuarioService)
        {
            _logger = logger;
            _articulosService = articulosService;
            _usuarioService = usuarioService;
        }


        public async Task<IActionResult> Index()
        {
         

            var articulos = await _articulosService.GetAsync("Articulos");

            // Calcular la suma de los valores de los artículos
            decimal suma = 0;
            foreach (var articulo in articulos)
            {
                suma += articulo.Unidad_Stock;
            }

            // Pasar la suma a la vista
            ViewBag.SumaArticulos = suma;


            //Calcular usuarios activos
          int totalUsuariosActivos = _usuarioService.GetTotalUsuariosActivos();

            //Pasar la suma de usuarios
         ViewBag.TotalUsuariosActivos = totalUsuariosActivos;

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
