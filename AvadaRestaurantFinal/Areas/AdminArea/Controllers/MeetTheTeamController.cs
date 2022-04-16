using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Extension;
using AvadaRestaurantFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class MeetTheTeamController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;
        public MeetTheTeamController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public IActionResult Index()
        {
            List<MeetTheTeam> MeetTheTeam = _context.MeetTheTeam.ToList();
            return View(MeetTheTeam);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetTheTeam meetTheTeam)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!meetTheTeam.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (meetTheTeam.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }

            string FileName = Guid.NewGuid() + meetTheTeam.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", FileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await meetTheTeam.Photo.CopyToAsync(fileStream);
            meetTheTeam.ImageUrl = FileName;

            await _context.MeetTheTeam.AddAsync(meetTheTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            var findId = _context.MeetTheTeam.Find(id);
            string path = Path.Combine(_env.WebRootPath, findId.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.MeetTheTeam.Remove(findId);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            MeetTheTeam dbMeetTheTeam = await _context.MeetTheTeam.FindAsync(id);
            if (dbMeetTheTeam == null) return NotFound();
            return View(dbMeetTheTeam);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, MeetTheTeam meetTheTeam)
        {
            if (id == null) return NotFound();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!meetTheTeam.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (meetTheTeam.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }
            MeetTheTeam dbMeetTheTeam = await _context.MeetTheTeam.FindAsync(id);
            string path = Path.Combine(_env.WebRootPath, dbMeetTheTeam.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string fileName = await meetTheTeam.Photo.SaveImageAsync(_env.WebRootPath, "img");
            dbMeetTheTeam.ImageUrl = fileName;
            dbMeetTheTeam.Name = meetTheTeam.Name;
            dbMeetTheTeam.profession = meetTheTeam.profession;
            dbMeetTheTeam.About = meetTheTeam.About;
            dbMeetTheTeam.ButtonName = meetTheTeam.ButtonName;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
