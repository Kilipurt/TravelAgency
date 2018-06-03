using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class HotelReservationDTO
    {
        public int HotelReservationId { get; set; }
        public HotelDTO Hotel { get; set; }
        public string ClientName { get; set; }
        public int NumberOfPersons { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
