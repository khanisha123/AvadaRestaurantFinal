using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using AvadaRestaurantFinal.ViewModels;
using Microsoft.AspNetCore.Http;
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
        public BasketController(Context context)
        {
            _context = context;
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
        public IActionResult ShowBasket()
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





        public IActionResult BasketCount([FromForm] int id, string change)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            var UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string basket = Request.Cookies["basketcookie"];
            List<BasketProduct> basketProducts = new List<BasketProduct>();
            basketProducts = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);
            Product product = _context.products.Find(id);
            var totalcount = 0;
            foreach (var item in basketProducts)
            {
                if (item.Id == id && item.UserId == UserId)
                {
                    if (change == "sub" && (item.Count) > 1)
                    {
                        item.Count--;
                        totalcount += item.Count;

                    }
                    
                    if (totalcount != 0) item.Count = totalcount;
                }

            }

            Response.Cookies.Append("basketcookie", JsonConvert.SerializeObject(basketProducts), new CookieOptions { MaxAge = TimeSpan.FromDays(14) });
            if (totalcount != 0)
            {
                return Ok(totalcount);
            }
            return Ok("error");
        }
        
        








    }
}
