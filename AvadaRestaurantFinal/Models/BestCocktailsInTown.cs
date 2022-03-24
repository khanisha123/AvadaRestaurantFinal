using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Models
{
    public class BestCocktailsInTown
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ButtonName { get; set; }
        public string ImageUrl { get; set; }
    }
}
