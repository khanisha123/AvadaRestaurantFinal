using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Do not leave empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Do not leave empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
