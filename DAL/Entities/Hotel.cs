using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual List<HotelReservation> Reservations { get; set; }

        public Hotel()
        {
            Reservations = new List<HotelReservation>();
        }
    }
}
