using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
