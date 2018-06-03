using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace Web.Models
{
    public class HotelReservationViewModel
    {
        [Display(Name = "Number")]
        public int HotelReservationId { get; set; }

        [Required]
        [Display(Name = "Hotel id")]
        public int HotelId { get; set; }

        [Display(Name = "Name of client")]
        [StringLength(50, MinimumLength = 3)]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Number of persons")]
        public int NumberOfPersons { get; set; }

        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }
    }
}