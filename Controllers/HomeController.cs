using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
