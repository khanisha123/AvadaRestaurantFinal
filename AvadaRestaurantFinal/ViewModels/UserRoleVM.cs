using AvadaRestaurantFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class UserRoleVM
    {
        public AppUser AppUser;
        //public IList<IdentityRole> Roles;
        public IList<string> Roles;
    }
}
