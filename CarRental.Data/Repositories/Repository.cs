using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CarRental.Entities;
using System.Collections.Generic;

namespace CarRental.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly CarRentalContext _context;

        public Repository(CarRentalContext context)
        {
            _context = context;
            
        }

        public IQueryable<T> GetAll<T>() where T : class
        { 
            return _context.Set<T>().AsQueryable();
        }

        public T  GetById<T>(int id) where T : class 
        {
            return _context.Set<T>().Find(id) ?? throw new InvalidOperationException($"Entity with id{id} not found");
        }

        public void Add<T>(T entity) where T :class
        {
            _context.Set<T>().Add(entity);

             
            if(entity is Reservation reservation && reservation.Car != null && string.IsNullOrEmpty(reservation.Car.BrandName))
            {
                var car = _context.Cars.Find(reservation.CarID);
                if(car != null)
                {
                    reservation.Car.BrandName = car.BrandName;
                }
            }
            Save<T>();
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity); 
            _context.Entry(entity).State = EntityState.Modified;
            Save<T>();
        }

        public void Delete<T>(int id) where T : class
        {
            T? entity = _context.Set<T>().Find(id);
            if(entity != null) 
            {
                _context.Set<T>().Remove(entity);
            Save<T>();
            }
            else
            {
                throw new InvalidOperationException($"Entiy with id {id} not found");
            }
    }

       public  void Save<T>()
        {
            _context.SaveChanges();
        }
    }
}
