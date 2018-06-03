using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL.EF
{
    public class TourDBContext : DbContext
    {
        public TourDBContext()
            : base("Tour")
        {

        }

        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelReservation> HotelReservations { get; set; }
    }
}
