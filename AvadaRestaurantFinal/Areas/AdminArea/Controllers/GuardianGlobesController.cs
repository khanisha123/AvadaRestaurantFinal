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
    public class GuardianGlobesController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _env;
        public GuardianGlobesController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<GuardianGlobe> guardianGlobes = _context.GuardianGlobe.ToList();
            return View(guardianGlobes);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GuardianGlobe guardianGlobe)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!guardianGlobe.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (guardianGlobe.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }

            string FileName = Guid.NewGuid() + guardianGlobe.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", FileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await guardianGlobe.Photo.CopyToAsync(fileStream);
            guardianGlobe.ImageUrl = FileName;

            await _context.GuardianGlobe.AddAsync(guardianGlobe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            var findId = _context.GuardianGlobe.Find(id);
            string path = Path.Combine(_env.WebRootPath, findId.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.GuardianGlobe.Remove(findId);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            GuardianGlobe dbGuardianGlobe = await _context.GuardianGlobe.FindAsync(id);
            if (dbGuardianGlobe == null) return NotFound();
            return View(dbGuardianGlobe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, GuardianGlobe guardianGlobe)
        {
            if (id == null) return NotFound();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!guardianGlobe.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (guardianGlobe.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }
            GuardianGlobe dbGuardianGlobe = await _context.GuardianGlobe.FindAsync(id);
            string path = Path.Combine(_env.WebRootPath, dbGuardianGlobe.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string fileName = await guardianGlobe.Photo.SaveImageAsync(_env.WebRootPath, "img");
            dbGuardianGlobe.ImageUrl = fileName;
            dbGuardianGlobe.Name = guardianGlobe.Name;
            dbGuardianGlobe.profession = guardianGlobe.profession;
            dbGuardianGlobe.Description = guardianGlobe.Description;
            dbGuardianGlobe.ButtonName = guardianGlobe.ButtonName;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
