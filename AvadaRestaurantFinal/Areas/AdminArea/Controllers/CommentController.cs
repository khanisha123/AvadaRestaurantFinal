using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly Context _context;
        public CommentController(Context context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            List<Comment> comments = _context.comments.Include(x=>x.appUser).Include(x=>x.bulletin).ToList();
            return View(comments);
        }
    }
}
