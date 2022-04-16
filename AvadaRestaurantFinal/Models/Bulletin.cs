using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Models
{
    public class Bulletin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ForDetailName { get; set; }
        public string ForDetailImageUrl { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public List<Comment> comments { get; set; }
    }
}
