using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int NumberOfGuest { get; set; }
        public DateTime DateOfReserVation { get; set; }
        public DateTime TimeOfReservation { get; set; }
        public string AdditionalNote { get; set; }
        public string AppUserId { get; set; }
        public AppUser appUser { get; set; }
        

    }
}
