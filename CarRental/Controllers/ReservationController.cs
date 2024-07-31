using Microsoft.AspNetCore.Mvc;
using CarRental.Services;
using CarRental.Entities;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRental.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationService _reservationService;
        private readonly CarService _carService;
        private readonly CustomerService _customerService;
        private readonly LocationService _locationService;

        public ReservationController(ReservationService reservationService, CarService carService, CustomerService customerService, LocationService locationService)
        {
            _reservationService = reservationService;
            _carService = carService;
            _customerService = customerService;
            _locationService = locationService;
        }

        private void FillViewBags()
        {
            ViewBag.Cars = _carService.GetAllCars().Select(car => new SelectListItem
            {
                Value = car.CarID.ToString(),
                Text = car.CarModel
            }).ToList();

            ViewBag.Customers = _customerService.GetAllCustomers().Select(customer => new SelectListItem
            {
                Value = customer.CustomerID.ToString(),
                Text = customer.FirstName + " " + customer.LastName
            }).ToList();

            ViewBag.Locations = _locationService.GetAllLocations().Select(location => new SelectListItem
            {
                Value = location.LocationID.ToString(),
                Text = location.LocationAddress
            }).ToList();
        }

        // GET: Reservation
        public IActionResult Index()
        {
            var reservations = _reservationService.GetAllReservations().ToList();
            return View(reservations);
        }

        // GET: Reservation/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _reservationService.GetReservationById(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            FillViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reservation reservation)
        {
            reservation.Status = "Pending";

            if (ModelState.IsValid)
            {
                FillViewBags();
                return View(reservation);
            }

            if (!ValidateReservation(reservation, out var validationResult))
            {
                ModelState.AddModelError(string.Empty, validationResult);
                FillViewBags();
                return View(reservation);
            }

            var rentalDays = (reservation.EndDate - reservation.StartDate).Days;
            reservation.TotalPrice = _carService.GetCarById(reservation.CarID).CarDailyPrice * rentalDays;

            
            _reservationService.CreateReservation(reservation);

            return RedirectToAction(nameof(Index));
        }

        // GET: Reservation/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _reservationService.GetReservationById(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            FillViewBags();
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                FillViewBags();
                return View(reservation);
            }

            if (!ValidateReservation(reservation, out var validationResult))
            {
                ModelState.AddModelError(string.Empty, validationResult);
                FillViewBags();
                return View(reservation);
            }

            var rentalDays = (reservation.EndDate - reservation.StartDate).Days;
            reservation.TotalPrice = _carService.GetCarById(reservation.CarID).CarDailyPrice * rentalDays;

            _reservationService.UpdateReservation(reservation);

            return RedirectToAction(nameof(Index));
        }

        // GET: Reservation/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _reservationService.GetReservationById(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var reservation = _reservationService.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _reservationService.DeleteReservation(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ValidateReservation(Reservation reservation, out string validationResult)
        {
            validationResult = string.Empty;

            var car = _carService.GetCarById(reservation.CarID);
            var customer = _customerService.GetCustomerById(reservation.CustomerID);
            var pickupLocation = _locationService.GetLocationById(reservation.PickupLocationID);
            var dropoffLocation = _locationService.GetLocationById(reservation.DropoffLocationID);

            if (car == null)
            {
                validationResult = "Invalid Car ID";
                return false;
            }

            if (customer == null)
            {
                validationResult = "Invalid Customer ID";
                return false;
            }

            if (pickupLocation == null || dropoffLocation == null)
            {
                validationResult = "Invalid Pickup or Dropoff location";
                return false;
            }

            return true;
        }

        [HttpPost]
        public IActionResult GetDailyPrice(int carId)
        {
            var car = _carService.GetCarById(carId);
            if (car != null)
            {
                return Json(new { dailyPrice = car.CarDailyPrice });
            }
            return Json(new { dailyPrice = 0 });
        }
    }
}
