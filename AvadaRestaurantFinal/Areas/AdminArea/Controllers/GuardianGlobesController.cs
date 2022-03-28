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
    public class GuardianGlobesController : Controller
    {
        private readonly Context _context;
        public GuardianGlobesController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<GuardianGlobe> guardianGlobes = _context.GuardianGlobe.ToList();
            return View(guardianGlobes);
        }
    }
}
