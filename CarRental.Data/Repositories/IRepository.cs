using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Data.Repositories
{
    public interface IRepository 
    {
        IQueryable<T> GetAll<T>() where T : class;
        T GetById<T>(int id) where T : class;
        void Add<T>(T entity)where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(int id)where T : class;
        void Save<T>();
    }
}
