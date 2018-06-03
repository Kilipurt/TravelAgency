using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public bool Transport { get; set; }
        public bool Hotel { get; set; }
        public int NumberOfPerson { get; set; }
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
