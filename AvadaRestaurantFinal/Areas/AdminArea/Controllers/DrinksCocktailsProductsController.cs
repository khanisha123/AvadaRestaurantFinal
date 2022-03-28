using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class DrinksCocktailsProductsController : Controller
    {
        private readonly Context _context;
        public DrinksCocktailsProductsController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<DrinksCocktailsProducts> DrinksCocktailsProducts = _context.DrinksCocktailsProducts.ToList();
            return View(DrinksCocktailsProducts);
        }
    }
}
