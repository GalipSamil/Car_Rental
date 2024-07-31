using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRental.Controllers
{
    public class HomeController : Controller 
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var role  = HttpContext.Session.GetString("Role");
            if (role == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            var customerId = HttpContext.Session.GetString("CustomerID");
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GotoCars()
        {
            return RedirectToAction("index", "Car");
        }

        public IActionResult GotoCustomers()
        {
            return RedirectToAction("index", "Customer");
        }

        public IActionResult GotoReservations()
        {
            return RedirectToAction("index", "Reservation");
        }
        public IActionResult GotoLocations()
        {
            return RedirectToAction("index", "Location");
        }
        public IActionResult GotoBrands()
        {
            return RedirectToAction("index", "Brand");
        }
        public IActionResult GotoProfiles()
        {
            return RedirectToAction("index", "Profile");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
