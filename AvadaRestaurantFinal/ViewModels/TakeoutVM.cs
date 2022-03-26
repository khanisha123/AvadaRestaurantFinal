using AvadaRestaurantFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewModels
{
    public class TakeoutVM
    {
        public List<HorsDoeuvresProduct> horsDoeuvresProduct { get; set; }
        public List<MainCourseProducts> MainCourseProducts { get; set; }
        public List<DessertCoffeeProducts> DessertCoffeeProducts { get; set; }
        public List<DrinksCocktailsProducts> DrinksCocktailsProducts { get; set; }
    }
}
