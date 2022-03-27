using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using AvadaRestaurantFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Controllers
{
    public class MenuController : Controller
    {
        private readonly Context _context;
        public MenuController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            MenuVM menuVM = new MenuVM();

            List<HorsDoeuvresProduct> horsDoeuvresProduct = _context.HorsDoeuvresProduct.ToList();
            List<MainCourseProducts> mainCourseProducts = _context.MainCourseProducts.ToList();
            List<DessertCoffeeProducts> dessertCoffeeProducts = _context.DessertCoffeeProducts.ToList();
            List<DrinksCocktailsProducts> drinksCocktailsProducts = _context.DrinksCocktailsProducts.ToList();
            menuVM.horsDoeuvresProduct = horsDoeuvresProduct;
            menuVM.DrinksCocktailsProducts = drinksCocktailsProducts;
            menuVM.MainCourseProducts = mainCourseProducts;
            menuVM.DessertCoffeeProducts = dessertCoffeeProducts;

            return View(menuVM);
        }
        public IActionResult HorsDoeuvresProductDetail(int? id)
        {
            HorsDoeuvresProduct horsDoeuvresProduct = _context.HorsDoeuvresProduct.FirstOrDefault(x=>x.Id == id);
            return View(horsDoeuvresProduct);
        }
        public IActionResult MainCourseProductsDetail(int? id)
        {
            MainCourseProducts mainCourseProducts = _context.MainCourseProducts.FirstOrDefault(x => x.Id == id);
            return View(mainCourseProducts);
        }
        public IActionResult DessertCoffeeProductsDetail(int? id)
        {
            DessertCoffeeProducts dessertCoffeeProducts = _context.DessertCoffeeProducts.FirstOrDefault(x => x.Id == id);
            return View(dessertCoffeeProducts);
        }
        public IActionResult DrinksCocktailsProductsDetail(int? id)
        {
            DrinksCocktailsProducts drinksCocktailsProducts = _context.DrinksCocktailsProducts.FirstOrDefault(x => x.Id == id);
            return View(drinksCocktailsProducts);
        }
    }
}
