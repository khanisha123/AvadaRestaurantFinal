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
    public class MembersTeamController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _env;
        public MembersTeamController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<MembersTeam> MembersTeam = _context.MembersTeam.ToList();
            return View(MembersTeam);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MembersTeam membersTeam)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!membersTeam.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (membersTeam.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }

            string FileName = Guid.NewGuid() + membersTeam.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", FileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await membersTeam.Photo.CopyToAsync(fileStream);
            membersTeam.ImageUrl = FileName;

            await _context.MembersTeam.AddAsync(membersTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            var findId = _context.MembersTeam.Find(id);
            string path = Path.Combine(_env.WebRootPath, findId.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.MembersTeam.Remove(findId);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            MembersTeam dbMembersTeam = await _context.MembersTeam.FindAsync(id);
            if (dbMembersTeam == null) return NotFound();
            return View(dbMembersTeam);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, MembersTeam membersTeam)
        {
            if (id == null) return NotFound();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!membersTeam.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (membersTeam.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }
            MembersTeam dbMembersTeamr = await _context.MembersTeam.FindAsync(id);
            string path = Path.Combine(_env.WebRootPath, dbMembersTeamr.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string fileName = await membersTeam.Photo.SaveImageAsync(_env.WebRootPath, "img");
            dbMembersTeamr.ImageUrl = fileName;
            dbMembersTeamr.Name = membersTeam.Name;
            dbMembersTeamr.profession = membersTeam.profession;
            dbMembersTeamr.Description = membersTeam.Description;
            dbMembersTeamr.UnderDescriptionText = membersTeam.UnderDescriptionText;
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
