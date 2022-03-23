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
    public class HistoryController : Controller
    {
        private readonly Context _context;
        public HistoryController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HistoryVM historyVM = new HistoryVM();
            HistoryHeader historyHeader = _context.historyHeader.FirstOrDefault();
            PullUpAChair pullUpAChair = _context.PullUpAChair.FirstOrDefault();
            fineDiningExperience fineDiningExperience = _context.FineDiningExperience.FirstOrDefault();
            TakeoutNowAvailableHistory takeoutNowAvailableHistory = _context.TakeoutNowAvailableHistory.FirstOrDefault();
            KungPaoChickenHistory kungPaoChickenHistory = _context.KungPaoChickenHistory.FirstOrDefault();
            List<MeetTheTeam> MeetTheTeam = _context.MeetTheTeam.ToList();
            historyVM.historyHeader = historyHeader;
            historyVM.MeetTheTeam = MeetTheTeam;
            historyVM.KungPaoChickenHistory = kungPaoChickenHistory;
            historyVM.fineDiningExperience = fineDiningExperience;
            historyVM.takeoutNowAvailableHistory = takeoutNowAvailableHistory;
            historyVM.PullUpAChair = pullUpAChair;
            return View(historyVM);
        }
    }
}
