using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class BasketProduct
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string DescriptionFront { get; set; }
        public int Count { get; set; }
        public int ProductCount { get; set; }
        public string UserId { get; set; }
    }
}
