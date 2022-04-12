using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Models
{
    public class SalesProduct
    {
        public int Id { get; set; }
        public int SalesId { get; set; }
        public Sales Sales { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }
    }
}
