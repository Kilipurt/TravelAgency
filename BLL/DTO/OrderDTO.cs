using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public bool Transport { get; set; }
        public bool Hotel { get; set; }
        public int NumberOfPerson { get; set; }
        public TourDTO Tour { get; set; }
        public int TourId { get; set; }
    }
}
