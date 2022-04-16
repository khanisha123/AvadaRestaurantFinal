using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Models
{
    public class AppUser:IdentityUser
    {
        [Required(ErrorMessage = "Do not leave empty"), StringLength(maximumLength: 50)]
        public string FullName { get; set; }
        public bool isActive { get; set; }
        public List<Sales> sales { get; set; }
        public List<Comment> comments { get; set; }
        public List<Reservation> reservations { get; set; }
        
    }
}
