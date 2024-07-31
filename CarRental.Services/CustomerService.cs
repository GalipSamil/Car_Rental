using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.Data.Repositories;
using CarRental.Entities;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;

namespace CarRental.Services
{
    public class CustomerService
    {
        private readonly IRepository _Repository;

        public CustomerService(IRepository repository)
        {
            _Repository = repository;
        }

        public List<CustomerDto> GetCustomersForDropdown()
        {
            return _Repository.GetAll<Customer>()
                .Select(c => new CustomerDto
                {
                    CustomerID = c.CustomerID,
                    Email = c.Email,
                    FullName = c.FirstName + " " + c.LastName
                })
                .ToList();
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _Repository.GetAll<Customer>();
        }

        public Customer GetCustomerById(int Id)
        {
            return _Repository.GetById<Customer>(Id);
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _Repository.GetAll<Customer>().SingleOrDefault(c => c.Email == email);
        }

        public Customer RegisterCustomer(RegisterViewModel model)
        {
            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                Password = HashPassword(model.Password)
            };
            _Repository.Add(customer);
            _Repository.Save<Customer>();
            return customer;
        }

        public Customer AuthenticateCustomer(string email, string password)
        {
            Console.WriteLine("AuthenticateCustomer method started"); 

            var customer = _Repository.GetAll<Customer>().SingleOrDefault(c => c.Email == email);

            if (customer == null)
            {
                Console.WriteLine("Customer not found"); 
                return null;
            }

            if (string.IsNullOrEmpty(customer.Password))
            {
                Console.WriteLine("Customer password is null or empty"); 
                return null;
            }

            if (!VerifyPassword(password, customer.Password))
            {
                Console.WriteLine("Password verification failed"); 
                return null;
            }

            Console.WriteLine("Customer authenticated successfully"); 
            return customer;
        }


        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public void CreateCustomer(Customer customer)
        {
            _Repository.Add(customer);
            _Repository.Save<Customer>();
        }

        public void UpdateCustomer(Customer customer)
        {
            _Repository.Update(customer);
            _Repository.Save<Customer>();
        }

        public void DeleteCustomer(int Id)
        {
            _Repository.Delete<Customer>(Id);
            _Repository.Save<Customer>();
        }

        public bool ChangePassword(int customerId, string newPassword)
        {
            var customer = _Repository.GetById<Customer>(customerId);
            if (customer != null)
            {
                customer.Password = HashPassword(newPassword);
                _Repository.Update(customer);
                _Repository.Save<Customer>();
                return true;
            }
            return false;
        }

        public bool SetResetCode(Customer customer, string code)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            customer.ResetCode = code;
            customer.ResetCodeExpiry = DateTime.Now.AddHours(1.0);
            _Repository.Update(customer);
            _Repository.Save<Customer>();
            return true;
        }

        public bool VerifyResetCode(Customer customer, string code)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (customer.ResetCode == code && customer.ResetCodeExpiry.HasValue && customer.ResetCodeExpiry.Value > DateTime.Now)
            {
                customer.ResetCode = null;
                customer.ResetCodeExpiry = null;
                _Repository.Update(customer);
                _Repository.Save<Customer>();
                return true;
            }
            return false;
        }

        public class CustomerDto
        {
            public int CustomerID { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
        }

        public Customer GetLoggedInCustomer(HttpContext httpContext)
        {
            var customerIdString = httpContext.Session.GetString("CustomerID");
            if (customerIdString == null)
            {
                return null;
            }

            int customerId = int.Parse(customerIdString);
            return GetCustomerById(customerId);
        }
    }
}
