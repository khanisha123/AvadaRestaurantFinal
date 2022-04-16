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
    public class MainCourseProductsController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;
        public MainCourseProductsController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public IActionResult Index()
        {
            List<MainCourseProducts> MainCourseProducts = _context.MainCourseProducts.ToList();
            return View(MainCourseProducts);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MainCourseProducts mainCourseProducts)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!mainCourseProducts.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (mainCourseProducts.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }

            string FileName = Guid.NewGuid() + mainCourseProducts.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", FileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await mainCourseProducts.Photo.CopyToAsync(fileStream);
            mainCourseProducts.ImageUrl = FileName;

            await _context.MainCourseProducts.AddAsync(mainCourseProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            var findId = _context.MainCourseProducts.Find(id);
            string path = Path.Combine(_env.WebRootPath, findId.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.MainCourseProducts.Remove(findId);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            MainCourseProducts dbMainCourseProducts = await _context.MainCourseProducts.FindAsync(id);
            if (dbMainCourseProducts == null) return NotFound();
            return View(dbMainCourseProducts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, MainCourseProducts mainCourseProducts)
        {
            if (id == null) return NotFound();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!mainCourseProducts.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (mainCourseProducts.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }
            MainCourseProducts dbmainCourseProducts = await _context.MainCourseProducts.FindAsync(id);
            string path = Path.Combine(_env.WebRootPath, dbmainCourseProducts.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string fileName = await mainCourseProducts.Photo.SaveImageAsync(_env.WebRootPath, "img");
            dbmainCourseProducts.ImageUrl = fileName;
            dbmainCourseProducts.Name = mainCourseProducts.Name;
            dbmainCourseProducts.Calories = mainCourseProducts.Calories;
            dbmainCourseProducts.CategoryName = mainCourseProducts.CategoryName;
            dbmainCourseProducts.GlutenFree = mainCourseProducts.GlutenFree;
            dbmainCourseProducts.HomeSideDescription = mainCourseProducts.HomeSideDescription;
            dbmainCourseProducts.Price = mainCourseProducts.Price;
            dbmainCourseProducts.LactoseFree = mainCourseProducts.LactoseFree;
            dbmainCourseProducts.DescriptionFront = mainCourseProducts.DescriptionFront;
            dbmainCourseProducts.TakeoutSideDescription = mainCourseProducts.TakeoutSideDescription;
            dbmainCourseProducts.HomeSideDescription = mainCourseProducts.HomeSideDescription;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
