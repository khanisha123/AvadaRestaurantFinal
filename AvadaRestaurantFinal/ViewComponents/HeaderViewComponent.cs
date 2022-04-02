﻿using AvadaRestaurantFinal.DAL;
using AvadaRestaurantFinal.Models;
using AvadaRestaurantFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(Context context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserName = appUser.FullName;
            };

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
