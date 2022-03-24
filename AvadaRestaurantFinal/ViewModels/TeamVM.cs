using AvadaRestaurantFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class TeamVM
    {
        public TeamHeader TeamHeader { get; set; }
        public List<MembersTeam> MembersTeam { get; set; }
    }
}
