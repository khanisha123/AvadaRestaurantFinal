using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Models
{
    public class DrinksCocktailsProducts
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string DescriptionFront { get; set; }
        public int Calories { get; set; }
        public string GlutenFree { get; set; }
        public string LactoseFree { get; set; }
        public string TakeoutSideDescription { get; set; }
        public string CategoryName { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
