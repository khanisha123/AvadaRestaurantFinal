using AvadaRestaurantFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class BulletinVM
    {
        public List<Bulletin> bulletins { get; set; }
        public Bulletin bulletin { get; set; }
        public Comment comment { get; set; }
        public List<Comment> comments { get; set; }
    }
}
