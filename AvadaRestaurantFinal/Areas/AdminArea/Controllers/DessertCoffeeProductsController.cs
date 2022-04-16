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
    public class DessertCoffeeProductsController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;
        public DessertCoffeeProductsController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public IActionResult Index()
        {
            List<DessertCoffeeProducts> DessertCoffeeProducts = _context.DessertCoffeeProducts.ToList();
            return View(DessertCoffeeProducts);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DessertCoffeeProducts dessertCoffeeProducts)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!dessertCoffeeProducts.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (dessertCoffeeProducts.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }

            string FileName = Guid.NewGuid() + dessertCoffeeProducts.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", FileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await dessertCoffeeProducts.Photo.CopyToAsync(fileStream);
            dessertCoffeeProducts.ImageUrl = FileName;

            await _context.DessertCoffeeProducts.AddAsync(dessertCoffeeProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            var findId = _context.DessertCoffeeProducts.Find(id);
            string path = Path.Combine(_env.WebRootPath, findId.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.DessertCoffeeProducts.Remove(findId);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            DessertCoffeeProducts dbDessertCoffeeProducts = await _context.DessertCoffeeProducts.FindAsync(id);
            if (dbDessertCoffeeProducts == null) return NotFound();
            return View(dbDessertCoffeeProducts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, DessertCoffeeProducts dessertCoffeeProducts)
        {
            if (id == null) return NotFound();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!dessertCoffeeProducts.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (dessertCoffeeProducts.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }
            DessertCoffeeProducts dbDessertCoffeeProducts = await _context.DessertCoffeeProducts.FindAsync(id);
            string path = Path.Combine(_env.WebRootPath, dbDessertCoffeeProducts.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string fileName = await dessertCoffeeProducts.Photo.SaveImageAsync(_env.WebRootPath, "img");
            dbDessertCoffeeProducts.ImageUrl = fileName;
            dbDessertCoffeeProducts.Name = dessertCoffeeProducts.Name;
            dbDessertCoffeeProducts.Calories = dessertCoffeeProducts.Calories;
            dbDessertCoffeeProducts.CategoryName = dessertCoffeeProducts.CategoryName;
            dbDessertCoffeeProducts.GlutenFree = dessertCoffeeProducts.GlutenFree;
            dbDessertCoffeeProducts.LactoseFree = dessertCoffeeProducts.LactoseFree;
            dbDessertCoffeeProducts.Price = dessertCoffeeProducts.Price;
            dbDessertCoffeeProducts.DescriptionFront = dessertCoffeeProducts.DescriptionFront;
            dbDessertCoffeeProducts.TakeoutSideDescription = dessertCoffeeProducts.TakeoutSideDescription;
            dbDessertCoffeeProducts.HomeSideDescription = dessertCoffeeProducts.HomeSideDescription;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
