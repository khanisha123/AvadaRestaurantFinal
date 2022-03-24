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
    public class TeamController : Controller
    {
        private readonly Context _context;
        public TeamController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TeamVM teamVM = new TeamVM();
            TeamHeader teamHeader= _context.TeamHeader.FirstOrDefault();
            List<MembersTeam> MembersTeam = _context.MembersTeam.ToList();
            teamVM.TeamHeader = teamHeader;
            teamVM.MembersTeam = MembersTeam;
            return View(teamVM);
        }
    }
}
