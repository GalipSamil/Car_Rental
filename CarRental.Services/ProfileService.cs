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
    public class ProfileService

    {
        private readonly IRepository _Repository;

        public ProfileService(IRepository repository)
        {
            _Repository = repository;
        }

        public IQueryable<Profile> GetAllProfiles()
        {
            return _Repository.GetAll<Profile>();
        }

        public Profile GetProfileById(int Id)
        {
            return _Repository.GetById<Profile>(Id);
        }
        public void CreateProfile(Profile profil)
        {
            _Repository.Add(profil);
            _Repository.Save<Profile>();
        }
        public void UpdateProfile(Profile profil)
        {
            _Repository.Update(profil);
            _Repository.Save<Profile>();
        }

        public void DeleteProfile(int Id)
        {

            _Repository.Delete<Profile>(Id);
            _Repository.Save<Profile>();
        }
    }
}
