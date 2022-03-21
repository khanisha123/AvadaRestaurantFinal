using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.ViewModels;
using AvadaRestaurantFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            fineDiningExperience fineDiningExperience = _context.FineDiningExperience.FirstOrDefault();
            foodIsOurCommon foodIsOurCommon = _context.foodIsOurCommons.FirstOrDefault();
            TheBestTableInTown theBestTableInTown = _context.theBestTableInTowns.FirstOrDefault();
            NewYorkTimes newYorkTimes = _context.newYorkTimes.FirstOrDefault();
            KungPao kungPao = _context.kungPao.FirstOrDefault();
            BraisedAbalone braisedAbalone = _context.braisedAbalone.FirstOrDefault();
            TakeoutNowAvailable takeoutNowAvailable = _context.takeoutNowAvailable.FirstOrDefault();
            List<GuardianGlobe> GuardianGlobe = _context.GuardianGlobe.ToList();
            homeVM.foodIsOurCommon = foodIsOurCommon;
            homeVM.braisedAbalone = braisedAbalone;
            homeVM.takeoutNowAvailables = takeoutNowAvailable;
            homeVM.guardianGlobes = GuardianGlobe;
            homeVM.NewYorkTimes = newYorkTimes;
            homeVM.TheBestTableInTown = theBestTableInTown;
            homeVM.kungPao = kungPao;
            homeVM.fineDiningExperience = fineDiningExperience;
            return View(homeVM);
        }
    }
}
