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
    public class TakeoutController : Controller
    {
        private readonly Context _context;
        public TakeoutController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //TakeoutVM takeoutVM = new TakeoutVM();
            //List<HorsDoeuvresProduct> horsDoeuvresProduct = _context.HorsDoeuvresProduct.ToList();
            //List<MainCourseProducts> mainCourseProducts = _context.MainCourseProducts.ToList();
            //List<DessertCoffeeProducts> dessertCoffeeProducts = _context.DessertCoffeeProducts.ToList();
            //List<DrinksCocktailsProducts> drinksCocktailsProducts = _context.DrinksCocktailsProducts.ToList();
            //takeoutVM.horsDoeuvresProduct = horsDoeuvresProduct;
            //takeoutVM.DrinksCocktailsProducts = drinksCocktailsProducts;
            //takeoutVM.MainCourseProducts = mainCourseProducts;
            //takeoutVM.DessertCoffeeProducts = dessertCoffeeProducts;
            List<Product> products = _context.products.ToList();
            return View(products);
        }
        public IActionResult ProductTakeoutDetail(int? id)
        {
            
            ProductTakeoutDetailVM productTakeoutDetailVM = new ProductTakeoutDetailVM();
            Product product = _context.products.FirstOrDefault(x => x.Id == id);
            List<Product> products1 = _context.products.Take(3).ToList();
            productTakeoutDetailVM.product = product;
            productTakeoutDetailVM.products = products1;
            return View(productTakeoutDetailVM);
        }
        //public IActionResult HorsDoeuvresProductTakeoutDetail(int? id)
        //{
        //    HorsDoeuvresProduct horsDoeuvresProduct = _context.HorsDoeuvresProduct.FirstOrDefault(x => x.Id == id);
        //    List<HorsDoeuvresProduct> horsDoeuvresProduct1 = _context.HorsDoeuvresProduct.Take(3).ToList();
        //    ViewBag.horsDoeuvresProduct1 = horsDoeuvresProduct1;
        //    return View(horsDoeuvresProduct);
        //}
        //public IActionResult MainCourseProductsTakeoutDetail(int? id)
        //{
        //    MainCourseProducts mainCourseProducts = _context.MainCourseProducts.FirstOrDefault(x => x.Id == id);
        //    List<MainCourseProducts> mainCourseProducts1 = _context.MainCourseProducts.Take(3).ToList();
        //    ViewBag.mainCourseProducts1 = mainCourseProducts1;
        //    return View(mainCourseProducts);
        //}
        //public IActionResult DessertCoffeeProductsTakeoutDetail(int? id)
        //{
        //    DessertCoffeeProducts dessertCoffeeProducts = _context.DessertCoffeeProducts.FirstOrDefault(x => x.Id == id);
        //    List<DessertCoffeeProducts> dessertCoffeeProducts1 = _context.DessertCoffeeProducts.Take(3).ToList();
        //    ViewBag.dessertCoffeeProducts1 = dessertCoffeeProducts1;
        //    return View(dessertCoffeeProducts);
        //}

    }
}
