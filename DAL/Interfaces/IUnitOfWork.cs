using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<Tour> Tours { get; }
        IGenericRepository<Hotel> Hotels { get; }
        IGenericRepository<HotelReservation> HotelReservations { get; }
        void Save();
    }
}
