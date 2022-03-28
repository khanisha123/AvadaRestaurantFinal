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
    public class BulettinController : Controller
    {
        private readonly Context _context;
        public BulettinController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Bulletin> bulletins = _context.Bulletin.ToList();
            return View(bulletins);
        }
    }
}
