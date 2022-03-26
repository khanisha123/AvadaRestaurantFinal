using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using AvadaRestaurantFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Controllers
{
    public class TakeoutController : Controller
    {
        private readonly Context _context;
        public TakeoutController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TakeoutVM takeoutVM = new TakeoutVM();
            List<HorsDoeuvresProduct> horsDoeuvresProduct = _context.HorsDoeuvresProduct.ToList();
            List<MainCourseProducts> mainCourseProducts = _context.MainCourseProducts.ToList();
            List<DessertCoffeeProducts> dessertCoffeeProducts = _context.DessertCoffeeProducts.ToList();
            List<DrinksCocktailsProducts> drinksCocktailsProducts = _context.DrinksCocktailsProducts.ToList();
            takeoutVM.horsDoeuvresProduct = horsDoeuvresProduct;
            takeoutVM.DrinksCocktailsProducts = drinksCocktailsProducts;
            takeoutVM.MainCourseProducts = mainCourseProducts;
            takeoutVM.DessertCoffeeProducts = dessertCoffeeProducts;
            return View(takeoutVM);
        }
    }
}
