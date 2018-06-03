using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace Web.Models
{
    public class OrderViewModel
    {
        [Display(Name = "Number")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tour id")]
        public int TourId { get; set; }

        public bool Transport { get; set; }
        public bool Hotel { get; set; }

        [Required]
        [Display(Name = "Number of persons")]
        public int NumberOfPerson { get; set; }

        [Display(Name = "Name of client")]
        [StringLength(50, MinimumLength = 3)]
        public string ClientName { get; set; }

        public TourViewModel Tour { get; set; }
    }
}