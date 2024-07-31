using Microsoft.AspNetCore.Mvc;
using CarRental.Services;
using CarRental.Entities;
using CarRental.Data;
using System.Linq;

namespace CarRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customer
        public IActionResult Index()
        {
            var customers = _customerService.GetAllCustomers().ToList();
            return View(customers);
        }

        // GET: Customer/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = _customerService.GetCustomerById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CustomerID,FirstName,LastName,Email,Phone,Address,Password")] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                _customerService.CreateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
           
            return View(customer);
        }

        // GET: Customer/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = _customerService.GetCustomerById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CustomerID,FirstName,LastName,Email,Phone,Address,Password")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _customerService.UpdateCustomer(customer);
                }
                catch
                {
                    if (!_customerService.GetAllCustomers().Any(e => e.CustomerID == id))
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
            return View(customer);
        }

        // GET: Customer/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = _customerService.GetCustomerById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            _customerService.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _customerService.GetAllCustomers().Any(e => e.CustomerID == id);
        }
    }
}

