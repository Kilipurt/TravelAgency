using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private TourDBContext db = new TourDBContext();
        private IGenericRepository<Order> orders;
        private IGenericRepository<Tour> tours;
        private IGenericRepository<Hotel> hotels;
        private IGenericRepository<HotelReservation> hotelReservations;

        public IGenericRepository<Order> Orders
        {
            get
            {
                if (this.orders == null)
                {
                    this.orders = new GenericRepository<Order>(db);
                }

                return orders;
            }
        }

        public IGenericRepository<Tour> Tours
        {
            get
            {
                if (this.tours == null)
                {
                    this.tours = new GenericRepository<Tour>(db);
                }

                return tours;
            }
        }

        public IGenericRepository<Hotel> Hotels
        {
            get
            {
                if (this.hotels == null)
                {
                    this.hotels = new GenericRepository<Hotel>(db);
                }

                return hotels;
            }
        }

        public IGenericRepository<HotelReservation> HotelReservations
        {
            get
            {
                if(this.hotelReservations == null)
                {
                    this.hotelReservations = new GenericRepository<HotelReservation>(db);
                }

                return hotelReservations;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
