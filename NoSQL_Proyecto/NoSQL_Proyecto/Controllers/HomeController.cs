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
        private readonly IngresosService _ingresosService;
        private readonly SalidasService _salidaService;
        private readonly ProveedorService _proveedorService;
        private readonly TipoArticulosService _tipoArticulosService;

        public HomeController(ILogger<HomeController> logger, ArticulosService articulosService, UsuarioService usuarioService, IngresosService ingresosService, SalidasService salidaService, ProveedorService proveedorService, TipoArticulosService tipoArticulosService)
        {
            _logger = logger;
            _articulosService = articulosService;
            _usuarioService = usuarioService;
            _ingresosService = ingresosService;
            _salidaService = salidaService;
            _proveedorService = proveedorService;
            _tipoArticulosService = tipoArticulosService;
        }


        public async Task<IActionResult> Index()
        {

            // Calcular la suma de los valores de los artículos

            var articulos = await _articulosService.GetAsync("Articulos");

         
            decimal suma = 0;
            foreach (var articulo in articulos)
            {
                suma += articulo.Unidad_Stock;
            }
            ViewBag.SumaArticulos = suma;


            //''''''''''''''''''''''''''''''''''''''''''''''''''''


            //Calcular usuarios activos

          int totalUsuariosActivos = _usuarioService.GetTotalUsuariosActivos();

         ViewBag.TotalUsuariosActivos = totalUsuariosActivos;


            //''''''''''''''''''''''''''''''''''''''''''''''''''''



            //Calcular suma de total ingresos

            int totalIngresos = await _ingresosService.GetTotalIngresos("Ingresos");

            ViewBag.TotalIngresos = totalIngresos;


            //''''''''''''''''''''''''''''''''''''''''''''''''''''


            //Calcular suma total salidas

            int totalSalidas = await _salidaService.GetTotalIngresos("Salidas");

            ViewBag.TotalSalidas = totalSalidas;

            //''''''''''''''''''''''''''''''''''''''''''''''''''''


            var recentProviders = await _proveedorService.GetRecentProvider("Proveedor");
            var recentTipos = await _tipoArticulosService.GetRecentTipo("Tipo_Articulos");

            var viewModel = new RecentDataViewModel
            {
                RecentProviders = recentProviders,
                RecentTipos = recentTipos
            };

            return View(viewModel);
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
