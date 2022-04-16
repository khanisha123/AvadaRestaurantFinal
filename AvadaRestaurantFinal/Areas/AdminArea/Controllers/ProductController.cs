using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Extension;
using AvadaRestaurantFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        
        public IActionResult Index()
        {
            List<Product> products = _context.products.Include(x => x.category).ToList();
            return View(products);
        }
        public IActionResult Create()
        {

            List<SelectListItem> d = (from x in _context.categories.ToList()
                                      select new SelectListItem
                                      {
                                          Text = x.Name,
                                          Value = x.Id.ToString()

                                      }).ToList();
            ViewBag.dgr = d;

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {


            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!product.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (product.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }


            var per = _context.categories.Where(x => x.Id == product.category.Id).FirstOrDefault();

            product.category = per;


            string FileName = Guid.NewGuid() + product.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", FileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await product.Photo.CopyToAsync(fileStream);
            product.ImageUrl = FileName;

            await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {

            var findID = _context.products.Find(id);
            _context.products.Remove(findID);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {

            List<SelectListItem> d = (from x in _context.categories.ToList()
                                      select new SelectListItem
                                      {
                                          Text = x.Name,
                                          Value = x.Id.ToString()

                                      }).ToList();

            ViewBag.dgr = d;

            if (id == null) return NotFound();
            var product = _context.products.Find(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product)
        {
            if (id == null) return NotFound();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!product.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (product.Photo.Length / 1024 > 300)
            {
                ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                return View();
            }
            Product dbProduct = await _context.products.FindAsync(id);
            string path = Path.Combine(_env.WebRootPath, dbProduct.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string fileName = await product.Photo.SaveImageAsync(_env.WebRootPath, "img");
            dbProduct.ImageUrl = fileName;
            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.Calories = product.Calories;
            dbProduct.GlutenFree = product.GlutenFree;
            dbProduct.LactoseFree = product.LactoseFree;
            dbProduct.DescriptionFront = product.DescriptionFront;
            dbProduct.HomeSideDescription = product.HomeSideDescription;
            dbProduct.TakeoutSideDescription = product.TakeoutSideDescription;
            
            var per = _context.categories.Where(x => x.Id == product.category.Id).FirstOrDefault();

            product.category = per;
            dbProduct.category = product.category;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
