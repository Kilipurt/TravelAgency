using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class FilterTourViewModel
    {
        [StringLength(50, MinimumLength = 3)]
        public string Country { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Region { get; set; }
        public int? Duration { get; set; }
        public Boolean isHotTours { get; set; }
    }
}