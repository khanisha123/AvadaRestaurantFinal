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
    public class MeetTheTeamController : Controller
    {
        private readonly Context _context;
        public MeetTheTeamController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<MeetTheTeam> MeetTheTeam = _context.MeetTheTeam.ToList();
            return View(MeetTheTeam);
        }
    }
}
