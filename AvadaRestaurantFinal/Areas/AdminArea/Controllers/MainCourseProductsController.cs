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
    public class MainCourseProductsController : Controller
    {
        private readonly Context _context;
        public MainCourseProductsController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<MainCourseProducts> MainCourseProducts = _context.MainCourseProducts.ToList();
            return View(MainCourseProducts);
        }
    }
}
