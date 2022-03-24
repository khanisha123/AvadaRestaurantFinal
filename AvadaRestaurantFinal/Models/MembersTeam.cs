using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Models
{
    public class MembersTeam
    {
        public int Id { get; set; }
        public string profession { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnderDescriptionText { get; set; }
    }
}
