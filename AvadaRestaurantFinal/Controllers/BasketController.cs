using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using AvadaRestaurantFinal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            HorsDoeuvresProduct horsDoeuvresProduct = await _context.HorsDoeuvresProduct.FindAsync(id);
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
            BasketProduct isExistProduct = basketProductsList.Find(p=>p.Id == horsDoeuvresProduct.Id);
            
            if (isExistProduct == null)
            {
                BasketProduct basketProduct = new BasketProduct
                {
                    Id=horsDoeuvresProduct.Id,
                   
                    Count = 1
                };
                basketProductsList.Add(basketProduct);
            }
            else
            {
                isExistProduct.Count++;
            }
            
            
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProductsList), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });
            return RedirectToAction("Index","Takeout");
        }
        public  IActionResult ShowBasket()
        {
            string basket = Request.Cookies["basket"];
            List<BasketProduct> products = new List<BasketProduct>();
            if (basket != null)
            {
                products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

                foreach (var item in products)
                {
                  HorsDoeuvresProduct horsDoeuvresProduct= _context.HorsDoeuvresProduct.FirstOrDefault(p=>p.Id==item.Id);
                    item.Price = horsDoeuvresProduct.Price;
                    item.ImageUrl = horsDoeuvresProduct.ImageUrl;
                    item.Name = horsDoeuvresProduct.Name;
                }
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });


            }

            return View(products);
        }
    }
}
