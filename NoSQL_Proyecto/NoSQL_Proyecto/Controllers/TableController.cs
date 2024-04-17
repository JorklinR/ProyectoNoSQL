using Microsoft.AspNetCore.Mvc;

namespace NoSQL_Proyecto.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
