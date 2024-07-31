using CarRental.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities;
using System.Net.Sockets;

namespace CarRental.Services
{
    public static class AdminSeeder
    {
        public static void SeedAdmin(CarRentalContext context)
        {
            if(!context.Customers.Any(c =>c.Role == "Admin"))
            {
                var admin = new Customer
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@carrental.com",
                    Phone = "543534543",
                    Address = "Adana",
                    Password = CustomerService.HashPassword("Admin123"),
                    Role = "Admin"
                };

                context.Customers.Add(admin);
                context.SaveChanges();

            }
            
        }
    }
}
