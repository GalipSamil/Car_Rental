using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CarRental.Data
{
    public class CarRentalContextFactory : IDesignTimeDbContextFactory<CarRentalContext>
    {
        public CarRentalContext CreateDbContext(string[] args)
        {
            // appsettings.json dosyasının CarRental projesinin kök dizininde olduğunu belirtiyoruz.
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CarRental"))
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<CarRentalContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new CarRentalContext(builder.Options);
        }
    }
}
