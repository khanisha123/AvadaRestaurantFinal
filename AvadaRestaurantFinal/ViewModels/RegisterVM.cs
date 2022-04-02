using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Do not leave empty"), StringLength(40)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Do not leave empty"), StringLength(40)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Do not leave empty"), StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Do not leave empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Do not leave empty")]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
