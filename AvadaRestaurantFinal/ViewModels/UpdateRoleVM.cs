using AvadaRestaurantFinal.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class UpdateRoleVM
    {
        public IList<IdentityRole> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
        public string Userid { get; set; }
        public AppUser User { get; set; }
    }
}
