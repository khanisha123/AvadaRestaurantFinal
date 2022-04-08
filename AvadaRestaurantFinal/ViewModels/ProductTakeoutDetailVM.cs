using AvadaRestaurantFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class ProductTakeoutDetailVM
    {
        public Product product { get; set; }
        public List<Product> products { get; set; }
        
    }
}
