using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using AvadaRestaurantFinal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Controllers
{
    public class BasketController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        public BasketController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Eror");
            }
            Product horsDoeuvresProduct = await _context.products.FindAsync(id);
            if (horsDoeuvresProduct == null)
            {
                return RedirectToAction("Index", "Eror");
            }
            List<BasketProduct> basketProductsList;
            string basket = Request.Cookies["basket"];
            if (basket == null)
            {
                basketProductsList = new List<BasketProduct>();
            }
            else
            {
                basketProductsList = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);
            }
            BasketProduct isExistProduct = basketProductsList.Find(p => p.Id == horsDoeuvresProduct.Id);

            if (isExistProduct == null)
            {
                BasketProduct basketProduct = new BasketProduct
                {
                    Id = horsDoeuvresProduct.Id,
                    UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                    Count = 1
                };
                basketProductsList.Add(basketProduct);
            }
            else
            {
                isExistProduct.Count++;
            }


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProductsList), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });
            return RedirectToAction("Index", "Takeout");
        }
        public IActionResult ShowBasket(int Id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            var UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userid = UserId;
            string basket = Request.Cookies["basket"];
            List<BasketProduct> products = new List<BasketProduct>();
            if (basket != null)
            {
                products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

                foreach (var item in products)
                {
                    Product horsDoeuvresProduct = _context.products.FirstOrDefault(p => p.Id == item.Id);
                    item.Price = horsDoeuvresProduct.Price;
                    item.ProductCount = horsDoeuvresProduct.Count;
                    item.ImageUrl = horsDoeuvresProduct.ImageUrl;
                    item.Name = horsDoeuvresProduct.Name;
                }
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });


            }

            

            return View(products);
        }
        public IActionResult Remove(int? id)
        {
            if (id == null) RedirectToAction("Index", "Error");
            Product product = _context.products.Find(id);
            string basketCookie = Request.Cookies["basket"];
            List<BasketProduct> basketProductList = JsonConvert.DeserializeObject<List<BasketProduct>>(basketCookie);
            BasketProduct isExistProduct = basketProductList.FirstOrDefault(p => p.Id == product.Id);
            basketProductList.Remove(isExistProduct);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProductList), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });
            return RedirectToAction("ShowBasket", "Basket");
        }

        public async Task<IActionResult> Sale()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login","Account");
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            Sales sales = new Sales();
            sales.AppUserId = user.Id;
            sales.SaleDate = DateTime.Now;
            List<BasketProduct> basketProducts = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies["basket"]);
            List<SalesProduct> salesProducts = new List<SalesProduct>();
            List<Product> dbProducts = new List<Product>();
            foreach (var item in basketProducts)
            {
                Product dbProduct = await _context.products.FindAsync(item.Id);
                if (dbProduct.Count < item.Count)
                {
                    TempData["Fail"] = $"{item.Name} bazada yoxdur";
                    return RedirectToAction("ShowBasket", "Basket");

                }
                
                dbProducts.Add(dbProduct);

            }
            double total=0;
            foreach (var item in basketProducts)
            {
                Product dbProduct = dbProducts.Find(p=>p.Id ==item.Id);
                await UpdateProductCount(dbProduct,item);
                SalesProduct salesProduct = new SalesProduct();
                salesProduct.SalesId = sales.Id;
                salesProduct.ProductId = dbProduct.Id;
                TempData["Success"] = "Okay";
                
                salesProducts.Add(salesProduct);
                total += item.Count * item.Price;


            }
            
           

            sales.salesProducts = salesProducts;
            sales.Total = total;
            await _context.Sales.AddAsync(sales);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
        private async Task UpdateProductCount(Product product, BasketProduct basketProduct)
        {
            product.Count = product.Count - basketProduct.Count;
            await _context.SaveChangesAsync();
        }














    }
}
