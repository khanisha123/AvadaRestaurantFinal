using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using AvadaRestaurantFinal.ViewModels;
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
    public class BulletinController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        public BulletinController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<Bulletin> bulletins = _context.Bulletin.ToList();
            return View(bulletins);
        }
        public async Task<IActionResult> BulletinDetail(int? id, int bulletinId,int Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.userId = user.Id;
            }
            
            Bulletin bulletinid = _context.Bulletin.Find(bulletinId);
            ViewBag.bulletinid = bulletinid;
            BulletinVM bulletinVM = new BulletinVM();
            Bulletin bulletin = _context.Bulletin.Include(x => x.comments).FirstOrDefault(x => x.Id == id);
            Comment comment = _context.comments.Include(x => x.appUser).FirstOrDefault();
            List<Comment> comments = _context.comments.Include(x => x.appUser).OrderByDescending(x=>x.CommentDate).ToList();
            List<Bulletin> bulletins = _context.Bulletin.Include(x => x.comments).Take(3).ToList();
            bulletinVM.bulletin = bulletin;
            bulletinVM.comments = comments;
            bulletinVM.bulletins = bulletins;
            bulletinVM.comment = comment;
            
            return View(bulletinVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> BulletinDetail(int id, Comment comment)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                Bulletin bulletin = _context.Bulletin.Find(id);
                comment.AppUserId = user.Id;
                comment.BulletinId = bulletin.Id;
                comment.CommentDate = DateTime.Now;
                _context.comments.Add(comment);
                _context.SaveChanges();
                return RedirectToAction("BulletinDetail", "Bulletin");
            }

        }
        public IActionResult BulletinDetailRemove(int id)
        {


            Comment findId = _context.comments.Find(id);
            _context.comments.Remove(findId);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
        public IActionResult Update(int Id)
        {
            var FindId = _context.comments.Find(Id);
            return View(FindId);
        }
        [HttpPost]
        [ActionName("Update")]
        public IActionResult UpdateComment(int id, Comment comment)
        {
            var findId = _context.comments.Find(id);
            findId.addComment = comment.addComment;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
