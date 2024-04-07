using Microsoft.AspNetCore.Mvc;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class TableController : Controller
    {
        private readonly ArticulosService _articulosService;

        public TableController(ArticulosService articulosService)
        {
            _articulosService = articulosService;
        }

        public IActionResult Tables()
        {
            var collectionName = "Articulos"; 
            var articulos = _articulosService.GetAsync(collectionName).Result;
            return View(articulos);
        }
    }
}
