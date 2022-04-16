using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int TableGuestCount { get; set; }
        public bool isReserved { get; set; }
        
    }
}
