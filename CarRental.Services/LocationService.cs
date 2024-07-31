using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CarRental.Data.Repositories;
using CarRental.Entities;

namespace CarRental.Services
{
    public class LocationService

    {
        private readonly IRepository _Repository;

        public LocationService(IRepository repository)
        {
            _Repository = repository;
        }

        public IQueryable<Location> GetAllLocations()
        {
            return _Repository.GetAll<Location>();
        }

        public Location GetLocationById(int Id)
        {
            return _Repository.GetById<Location>(Id);
        }
        public void CreateLocation(Location location)
        {
            if (string.IsNullOrWhiteSpace(location.LocationName) ||
        string.IsNullOrWhiteSpace(location.LocationAddress) ||
        string.IsNullOrWhiteSpace(location.LocationCity) ||
        string.IsNullOrWhiteSpace(location.LocationZipCode) ||
        string.IsNullOrWhiteSpace(location.LocationCountry))
            {
                throw new ArgumentException("All location fields are required");
            }
            _Repository.Add(location);
            _Repository.Save<Location>();
        }
        public void UpdateCar(Location location)
        {
            _Repository.Update(location);
            _Repository.Save<Location>();
        }

        public void DeleteLocation(int Id)
        {

            _Repository.Delete<Location>(Id);
            _Repository.Save<Location>();
        }
    }
}
