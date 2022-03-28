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

    public class HorsDoeuvresProductController : Controller
    {
        private readonly Context _context;
        public HorsDoeuvresProductController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<HorsDoeuvresProduct> horsDoeuvresProducts = _context.HorsDoeuvresProduct.ToList();
            return View(horsDoeuvresProducts);
        }
    }
}
