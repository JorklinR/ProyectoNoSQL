using Microsoft.AspNetCore.Mvc;

namespace NoSQL_Proyecto.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
