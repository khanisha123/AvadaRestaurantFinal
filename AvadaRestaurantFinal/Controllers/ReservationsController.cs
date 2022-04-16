using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        public ReservationsController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            
            Reservation reservation = _context.reservations.FirstOrDefault();
            return View(reservation);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Index")]
        public async Task<IActionResult> CreateReservation(Reservation reservation,int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                
               
                    
                    AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    reservation.AppUserId = user.Id;
                    _context.reservations.Add(reservation);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
             
                
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<IActionResult> ShowReservation()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.userId = user.Id;
            }
            List<Reservation> reservations = _context.reservations.Include(x=>x.appUser).ToList();
            return View(reservations);
        }
    }
}
