using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.Entities;
using CarRental.Data.Repositories;

namespace CarRental.Services
{
    public class BrandService
    {
        private readonly IRepository _Repository;

        public BrandService(IRepository repository)
        {
            _Repository = repository;
        }

        public IQueryable<Brand> GetAllBrands()
        {
            return _Repository.GetAll<Brand>();
        }

        public Brand GetBrandById(int id)
        {
            return _Repository.GetById<Brand>(id);
        }

        public void CreateBrand(Brand brand) // 

        {   _Repository.Add(brand);
            _Repository.Save<Brand>();
        }

        public void UpdateBrand(Brand brand)
        {
            _Repository.Update(brand);
            _Repository.Save<Brand>();
        }

        public void DeleteBrand(int id)
        {
            _Repository.Delete<Brand>(id);
            _Repository.Save<Brand>();
        }

    }
}
