using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HotelReservation
    {
        public int HotelReservationId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public string ClientName { get; set; }
        public int NumberOfPersons { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
