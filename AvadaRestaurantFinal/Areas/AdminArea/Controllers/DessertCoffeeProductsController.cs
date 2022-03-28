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
    public class DessertCoffeeProductsController : Controller
    {
        private readonly Context _context;
        public DessertCoffeeProductsController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<DessertCoffeeProducts> DessertCoffeeProducts = _context.DessertCoffeeProducts.ToList();
            return View(DessertCoffeeProducts);
        }
    }
}
