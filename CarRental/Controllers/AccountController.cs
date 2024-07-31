using CarRental.Models;
using CarRental.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using CarRental.Entities;

namespace CarRental.Controllers
{
    public class AccountController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountController(CustomerService customerService, EmailService emailService, IConfiguration configuration)
        {
            _customerService = customerService;
            _emailService = emailService;
            _configuration = configuration;
        }

        // Get: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // Post: Account/Register 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.RegisterCustomer(model);
                if (customer != null)
                {
                    HttpContext.Session.SetString("CustomerID", customer.CustomerID.ToString());
                    HttpContext.Session.SetString("Role", customer.Role ?? string.Empty);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Registration failed");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Login method started"); 

                var customer = _customerService.AuthenticateCustomer(model.Email, model.Password);

                if (customer != null)
                {
                    HttpContext.Session.SetString("CustomerID", customer.CustomerID.ToString());
                    HttpContext.Session.SetString("Role", customer.Role);

                    if(customer.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    else if (customer.Role == "Customer")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Profile()
        {
            var customer = _customerService.GetLoggedInCustomer(HttpContext);
            if (customer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new ProfileViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                DriverLicenceNo = customer.Profile?.DriverLicenceNo
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetLoggedInCustomer(HttpContext);

                if (customer == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Email = model.Email;
                customer.Phone = model.Phone;

                if (customer.Profile == null)
                {
                    customer.Profile = new Profile
                    {
                        CustomerID = customer.CustomerID
                    };
                }

                customer.Profile.Email = model.Email;
                customer.Profile.FullName = $"{model.FirstName} {model.LastName}";
                customer.Profile.DriverLicenceNo = model.DriverLicenceNo;

                _customerService.UpdateCustomer(customer);

                return RedirectToAction("Profile");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var code = new Random().Next(100000, 999999).ToString();
                var customer = _customerService.GetCustomerByEmail(model.Email);

                if (customer != null)
                {
                    _customerService.SetResetCode(customer, code);
                    _emailService.SendEmail(model.Email, "Password Reset Code", $"Your password reset code is: {code}");
                    return RedirectToAction("ResetPassword", new { email = model.Email });
                }
                else
                {
                    ModelState.AddModelError("", "Email not found.");
                }
            }
            return View(model);
        }

        // GET: Account/ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            var model = new ResetPasswordViewModel { Email = email };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetCustomerByEmail(model.Email);

                if (customer != null)
                {
                    if (_customerService.VerifyResetCode(customer, model.Code)) 
                    {
                        bool result = _customerService.ChangePassword(customer.CustomerID, model.NewPassword);
                        if (result)
                        {
                            return RedirectToAction("ResetPasswordConfirmation");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Password change failed.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid reset code.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid customer or customer not found.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetLoggedInCustomer(HttpContext);
                if (customer != null && CustomerService.VerifyPassword(model.CurrentPassword, customer.Password))
                {
                    bool result = _customerService.ChangePassword(customer.CustomerID, model.NewPassword);
                    if (result)
                    {
                        ViewBag.Message = "Password changed successfully.";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Password change failed.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Current password is incorrect.");
                }
            }
            return View(model);
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Account");
        }
    }
}
