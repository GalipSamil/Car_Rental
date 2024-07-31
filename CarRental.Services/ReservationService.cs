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
    public class ReservationService

    {
        private readonly IRepository _Repository;

        public ReservationService(IRepository repository)
        {
            _Repository = repository;
        }

        public IQueryable<Reservation> GetAllReservations()
        {
            return _Repository.GetAll<Reservation>();
        }

        public Reservation GetReservationById(int Id)
        {
            return _Repository.GetById<Reservation>(Id);
        }

        public void CreateReservation(Reservation reservation)
        {
            var customer = _Repository.GetById<Customer>(reservation.CustomerID);
            var car = _Repository.GetById<Car>(reservation.CarID);
            var pickupLocation = _Repository.GetById<Location>(reservation.PickupLocationID);
            var dropoffLocation = _Repository.GetById<Location>(reservation.DropoffLocationID);

            if (reservation == null || customer == null || car == null || pickupLocation == null || dropoffLocation == null)
            {
                throw new ArgumentNullException(nameof(reservation));
            }

            reservation.Customer = customer;
            reservation.Car = car;
            reservation.PickupLocation = pickupLocation;
            reservation.DropoffLocation = dropoffLocation;

            _Repository.Add(reservation);
            _Repository.Save<Reservation>();
        }


        public void UpdateReservation(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null.");
            }

            var car = _Repository.GetById<Car>(reservation.CarID);
            if (car == null)
            {
                throw new InvalidOperationException($"Car with ID {reservation.CarID} not found.");
            }

            var customer = _Repository.GetById<Customer>(reservation.CustomerID);
            if (customer == null)
            {
                throw new InvalidOperationException($"Customer with ID {reservation.CustomerID} not found.");
            }

            reservation.Car = car;
            reservation.Customer = customer;

            _Repository.Update(reservation);
            _Repository.Save<Reservation>();
        }

        public void DeleteReservation(int id)
        {
            var reservation = _Repository.GetById<Reservation>(id);
            if (reservation == null)
            {
                throw new InvalidOperationException($"Reservation with ID {id} not found.");
            }

            _Repository.Delete<Reservation>(id);
            _Repository.Save<Reservation>();
        }
    }

}






