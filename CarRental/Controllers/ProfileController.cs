using Microsoft.AspNetCore.Mvc;
using CarRental.Services;
using CarRental.Entities;
using CarRental.DTOs;
using System.Linq;

namespace CarRental.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileService _profileService;
        private readonly CustomerService _customerService;

        public ProfileController(ProfileService profileService, CustomerService customerService)
        {
            _profileService = profileService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var profiles = _profileService.GetAllProfiles().ToList();
            return View(profiles);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var profile = _profileService.GetProfileById(id.Value);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        public IActionResult Create()
        {
            return View(new ProfileDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProfileDTO profileDTO)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetCustomerByEmail(profileDTO.Email);
                if (customer != null)
                {
                    var profile = new Profile
                    {
                        FullName = customer.FirstName + " " + customer.LastName,
                        Email = profileDTO.Email,
                        DriverLicenceNo = profileDTO.DriverLicenceNo,
                        CustomerID = customer.CustomerID
                    };

                    _profileService.CreateProfile(profile);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Customer not found.");
                }
            }
            return View(profileDTO);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var profile = _profileService.GetProfileById(id.Value);
            if (profile == null)
            {
                return NotFound();
            }

            var profileDTO = new ProfileDTO
            {
                ProfileID = profile.ProfileID,
                CustomerID = profile.CustomerID,
                FullName = profile.FullName,
                Email = profile.Email,
                DriverLicenceNo = profile.DriverLicenceNo
            };

            return View(profileDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProfileDTO profileDTO)
        {
            if (id != profileDTO.ProfileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var profile = new Profile
                    {
                        ProfileID = profileDTO.ProfileID,
                        CustomerID = profileDTO.CustomerID,
                        FullName = profileDTO.FullName,
                        Email = profileDTO.Email,
                        DriverLicenceNo = profileDTO.DriverLicenceNo
                    };

                    _profileService.UpdateProfile(profile);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    if (_profileService.GetProfileById(profileDTO.ProfileID) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(profileDTO);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var profile = _profileService.GetProfileById(id.Value);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var profile = _profileService.GetProfileById(id);
            if (profile == null)
            {
                return NotFound();
            }
            _profileService.DeleteProfile(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return _profileService.GetAllProfiles().Any(e => e.ProfileID == id);
        }
    }
}
