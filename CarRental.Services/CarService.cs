using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CarRental.Data.Repositories;
using CarRental.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Services
{
    public  class CarService 

    {
        private readonly IRepository _Repository;

        public CarService(IRepository repository)
        {
            _Repository = repository;
        }

        public IQueryable<Car> GetAllCars()
        {
            var query = from car in _Repository.GetAll<Car>()
                        join brand in _Repository.GetAll<Brand>() on car.BrandID equals brand.BrandID
                        select car;
            return query;
        }

        public Car GetCarById(int Id)
        {
            return _Repository.GetAll<Car>()
                              .Include(car => car.Brand) 
                              .FirstOrDefault(car => car.CarID == Id);
        }

        public void CreateCar(Car car)
        {
            var brand = _Repository.GetById<Brand>(car.BrandID);
            car.BrandName = brand.BrandName;
            _Repository.Add(car);
            _Repository.Save<Car>();
        }

        public void UpdateCar(Car car)
        {
            var brand = _Repository.GetById<Brand>(car.BrandID);
            car.BrandName = brand.BrandName; 
            _Repository.Update(car);
            _Repository.Save<Car>();
        }

        public void DeleteCar(int Id)
        {
            
            _Repository.Delete<Car>(Id);
            _Repository.Save<Car>();
        }
    }
}
