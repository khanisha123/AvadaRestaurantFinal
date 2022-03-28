using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Extension;
using AvadaRestaurantFinal.Models;
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

    public class HorsDoeuvresProductController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;
        public HorsDoeuvresProductController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public IActionResult Index()
        {
            List<HorsDoeuvresProduct> horsDoeuvresProducts = _context.HorsDoeuvresProduct.ToList();
            return View(horsDoeuvresProducts);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HorsDoeuvresProduct horsDoeuvresProduct)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!horsDoeuvresProduct.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (horsDoeuvresProduct.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }

            string FileName = Guid.NewGuid() + horsDoeuvresProduct.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", FileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await horsDoeuvresProduct.Photo.CopyToAsync(fileStream);
            horsDoeuvresProduct.ImageUrl = FileName;

            await _context.HorsDoeuvresProduct.AddAsync(horsDoeuvresProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            var findId = _context.HorsDoeuvresProduct.Find(id);
            string path = Path.Combine(_env.WebRootPath, findId.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.HorsDoeuvresProduct.Remove(findId);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            HorsDoeuvresProduct dbHorsDoeuvresProduct = await _context.HorsDoeuvresProduct.FindAsync(id);
            if (dbHorsDoeuvresProduct == null) return NotFound();
            return View(dbHorsDoeuvresProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, HorsDoeuvresProduct horsDoeuvresProduct)
        {
            if (id == null) return NotFound();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!horsDoeuvresProduct.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (horsDoeuvresProduct.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }
            HorsDoeuvresProduct dbHorsDoeuvresProduct = await _context.HorsDoeuvresProduct.FindAsync(id);
            string path = Path.Combine(_env.WebRootPath, dbHorsDoeuvresProduct.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string fileName = await horsDoeuvresProduct.Photo.SaveImageAsync(_env.WebRootPath, "img");
            dbHorsDoeuvresProduct.ImageUrl = fileName;
            dbHorsDoeuvresProduct.Name = horsDoeuvresProduct.Name;
            dbHorsDoeuvresProduct.Calories = horsDoeuvresProduct.Calories;
            dbHorsDoeuvresProduct.CategoryName = horsDoeuvresProduct.CategoryName;
            dbHorsDoeuvresProduct.GlutenFree = horsDoeuvresProduct.GlutenFree;
            dbHorsDoeuvresProduct.HomeSideDescription= horsDoeuvresProduct.HomeSideDescription;
            dbHorsDoeuvresProduct.Price = horsDoeuvresProduct.Price;
            dbHorsDoeuvresProduct.DescriptionFront = horsDoeuvresProduct.DescriptionFront;
            dbHorsDoeuvresProduct.TakeoutSideDescription = horsDoeuvresProduct.TakeoutSideDescription;
            dbHorsDoeuvresProduct.HomeSideDescription = horsDoeuvresProduct.HomeSideDescription;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
