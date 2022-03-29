using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.ViewComponents
{
    
    public class HeaderViewComponent : ViewComponent
    {
        private readonly Context _context;

        public HeaderViewComponent(Context context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            ViewBag.ProductCount = 0;
            if (Request.Cookies["basket"] != null)
            {
                double total = 0;
                List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies["basket"]);
                ViewBag.ProductCount = products.Count;
                foreach (var item in products)
                {
                    total += item.Count;
                }
                ViewBag.ProductCount = total;
            }

            return View();
        }
    }
}
