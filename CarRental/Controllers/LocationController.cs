using Microsoft.AspNetCore.Mvc;
using CarRental.Entities;
using CarRental.Services;
using System.Linq;

namespace CarRental.Controllers
{
    public class LocationController : Controller
    {
        private readonly LocationService _locationService;

        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: Location
        public IActionResult Index()
        {
            var locations = _locationService.GetAllLocations().ToList();
            return View(locations);
        }

        // GET: Location/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = _locationService.GetLocationById(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("LocationID,LocationName,LocationAddress,LocationCity,LocationZipCode,LocationCountry")] Location location)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Key: {modelState.Key}, Error: {error.ErrorMessage}");
                    }
                }

                return View(location);
            }

            _locationService.CreateLocation(location);
            return RedirectToAction(nameof(Index));
        }

        // GET: Location/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = _locationService.GetLocationById(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LocationID,LocationName,LocationAddress,LocationCity,LocationZipCode,LocationCountry")] Location location)
        {
            if (id != location.LocationID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Key: {modelState.Key}, Error: {error.ErrorMessage}");
                    }
                }

                return View(location);
            }

            _locationService.UpdateCar(location);
            return RedirectToAction(nameof(Index));
        }

        // GET: Location/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = _locationService.GetLocationById(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var location = _locationService.GetLocationById(id);
            if (location == null)
            {
                return NotFound();
            }

            _locationService.DeleteLocation(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _locationService.GetAllLocations().Any(e => e.LocationID == id);
        }
    }
}
