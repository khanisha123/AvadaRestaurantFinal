using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using AvadaRestaurantFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Controllers
{
    public class ReceptionBarController : Controller
    {
        private readonly Context _context;
        public ReceptionBarController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ReceptionBarVM receptionBarVM = new ReceptionBarVM();
            ReceptionBarHeader receptionBarHeader = _context.ReceptionBarHeader.FirstOrDefault();
            PlanYourReception planYourReception = _context.PlanYourReception.FirstOrDefault();
            BestCocktailsInTown bestCocktailsInTown = _context.bestCocktailsInTown.FirstOrDefault();
            PerfectPlaceForAReception perfectPlaceForAReception = _context.PerfectPlaceForAReception.FirstOrDefault();
            ReceptionBarEndSection receptionBarEndSection = _context.ReceptionBarEndSection.FirstOrDefault();
            List<FineDiningProfesionnal> FineDiningProfesionnal = _context.FineDiningProfesionnal.ToList();
            receptionBarVM.receptionBarHeader = receptionBarHeader;
            receptionBarVM.ReceptionBarEndSection = receptionBarEndSection;
            receptionBarVM.bestCocktailsInTown = bestCocktailsInTown;
            receptionBarVM.perfectPlaceForAReception = perfectPlaceForAReception;
            receptionBarVM.planYourReception = planYourReception;
            receptionBarVM.FineDiningProfesionnal = FineDiningProfesionnal;
            return View(receptionBarVM);
        }
    }
}
