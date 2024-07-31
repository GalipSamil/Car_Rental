using Microsoft.AspNetCore.Mvc;
using CarRental.Services;
using CarRental.Entities;
using CarRental.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        private readonly CarService _carService;
        private readonly BrandService _brandService;

        public CarController(CarService carService, BrandService brandService)
        {
            _carService = carService;
            _brandService = brandService;
        }

        // GET: Car
        public IActionResult Index()
        {
            var cars = _carService.GetAllCars().ToList();
            return View(cars);
        }

        // GET: Car/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carService.GetCarById(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        public IActionResult Create()
        {
            var brands = _brandService.GetAllBrands()
                                      .Select(b => new SelectListItem
                                      {
                                          Value = b.BrandName,
                                          Text = b.BrandName
                                      }).ToList();
            ViewBag.Brands = new SelectList(brands, "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarDTO carDTO)
        {
            if (ModelState.IsValid)
            {
                var brand = _brandService.GetAllBrands().FirstOrDefault(b => b.BrandName == carDTO.BrandName);
                if (brand != null)
                {
                    var car = new Car
                    {
                        BrandID = brand.BrandID,
                        CarType = carDTO.CarType,
                        CarModel = carDTO.CarModel,
                        CarTransmission = carDTO.CarTransmission,
                        CarFuel = carDTO.CarFuel,
                        CarMileage = carDTO.CarMileage,
                        CarDailyPrice = carDTO.CarDailyPrice,
                        CarLicencePlate = carDTO.CarLicencePlate
                    };
                    _carService.CreateCar(car);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("BrandName", "Selected brand does not exist.");
                }
            }

            var brands = _brandService.GetAllBrands()
                                      .Select(b => new SelectListItem
                                      {
                                          Value = b.BrandName,
                                          Text = b.BrandName
                                      }).ToList();
            ViewBag.Brands = new SelectList(brands, "Value", "Text", carDTO.BrandName);
            return View(carDTO);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carService.GetCarById(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            var carDTO = new CarDTO
            {
                CarID = car.CarID,
                BrandName = car.Brand.BrandName,
                CarType = car.CarType,
                CarModel = car.CarModel,
                CarTransmission = car.CarTransmission,
                CarFuel = car.CarFuel,
                CarMileage = car.CarMileage,
                CarDailyPrice = car.CarDailyPrice,
                CarLicencePlate = car.CarLicencePlate
            };

            var brands = _brandService.GetAllBrands()
                                      .Select(b => new SelectListItem
                                      {
                                          Value = b.BrandName,
                                          Text = b.BrandName
                                      }).ToList();
            ViewBag.Brands = new SelectList(brands, "Value", "Text", car.BrandName);
            return View(carDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CarDTO carDTO)
        {
            if (id != carDTO.CarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var brand = _brandService.GetAllBrands().FirstOrDefault(b => b.BrandName == carDTO.BrandName);
                if (brand != null)
                {
                    var car = new Car
                    {
                        CarID = carDTO.CarID,
                        BrandID = brand.BrandID,
                        CarType = carDTO.CarType,
                        CarModel = carDTO.CarModel,
                        CarTransmission = carDTO.CarTransmission,
                        CarFuel = carDTO.CarFuel,
                        CarMileage = carDTO.CarMileage,
                        CarDailyPrice = carDTO.CarDailyPrice,
                        CarLicencePlate = carDTO.CarLicencePlate
                    };
                    try
                    {
                        _carService.UpdateCar(car);
                    }
                    catch
                    {
                        if (!_carService.GetAllCars().Any(c => c.CarID == car.CarID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("BrandName", "Selected brand does not exist.");
                }
            }

            var brands = _brandService.GetAllBrands()
                                      .Select(b => new SelectListItem
                                      {
                                          Value = b.BrandName,
                                          Text = b.BrandName
                                      }).ToList();
            ViewBag.Brands = new SelectList(brands, "Value", "Text", carDTO.BrandName);
            return View(carDTO);
        }

        // GET: Car/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carService.GetCarById(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _carService.GetAllCars().Any(e => e.CarID == id);
        }
    }
}
