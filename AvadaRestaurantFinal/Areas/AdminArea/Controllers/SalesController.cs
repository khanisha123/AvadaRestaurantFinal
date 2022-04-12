using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize]
    public class SalesController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        public SalesController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            List<Sales> sales = _context.Sales
                .Include(x => x.AppUser)
                .Include(x=>x.salesProducts)
                .ThenInclude(x=>x.product).ToList();
            return View(sales);
        }
    }
}
