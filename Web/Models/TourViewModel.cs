using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using System.Data;

namespace Web.Models
{
    public class TourViewModel
    {
        [Display(Name = "Number")]
        public int TourId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Country { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Region { get; set; }

        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Hotel { get; set; }
    }
}