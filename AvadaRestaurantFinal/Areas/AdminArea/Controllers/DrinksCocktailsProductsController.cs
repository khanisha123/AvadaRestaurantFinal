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
    public class DrinksCocktailsProductsController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;
        public DrinksCocktailsProductsController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public IActionResult Index()
        {
            List<DrinksCocktailsProducts> DrinksCocktailsProducts = _context.DrinksCocktailsProducts.ToList();
            return View(DrinksCocktailsProducts);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrinksCocktailsProducts drinksCocktailsProducts)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!drinksCocktailsProducts.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (drinksCocktailsProducts.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }

            string FileName = Guid.NewGuid() + drinksCocktailsProducts.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", FileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await drinksCocktailsProducts.Photo.CopyToAsync(fileStream);
            drinksCocktailsProducts.ImageUrl = FileName;

            await _context.DrinksCocktailsProducts.AddAsync(drinksCocktailsProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            var findId = _context.DrinksCocktailsProducts.Find(id);
            string path = Path.Combine(_env.WebRootPath, findId.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.DrinksCocktailsProducts.Remove(findId);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            DrinksCocktailsProducts dbDrinksCocktailsProducts = await _context.DrinksCocktailsProducts.FindAsync(id);
            if (dbDrinksCocktailsProducts == null) return NotFound();
            return View(dbDrinksCocktailsProducts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, DrinksCocktailsProducts drinksCocktailsProducts)
        {
            if (id == null) return NotFound();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!drinksCocktailsProducts.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (drinksCocktailsProducts.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }
            DrinksCocktailsProducts dbDrinksCocktailsProducts = await _context.DrinksCocktailsProducts.FindAsync(id);
            string path = Path.Combine(_env.WebRootPath, dbDrinksCocktailsProducts.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string fileName = await drinksCocktailsProducts.Photo.SaveImageAsync(_env.WebRootPath, "img");
            dbDrinksCocktailsProducts.ImageUrl = fileName;
            dbDrinksCocktailsProducts.Name = drinksCocktailsProducts.Name;
            dbDrinksCocktailsProducts.CategoryName = drinksCocktailsProducts.CategoryName;
            dbDrinksCocktailsProducts.Price = drinksCocktailsProducts.Price;
            dbDrinksCocktailsProducts.DescriptionFront = drinksCocktailsProducts.DescriptionFront;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
