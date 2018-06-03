using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Tour
    {
        public int TourId { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Hotel { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
