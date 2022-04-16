using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string DescriptionFront { get; set; }
        public int Calories { get; set; }
        public string GlutenFree { get; set; }
        public string LactoseFree { get; set; }
        public string TakeoutSideDescription { get; set; }
        public string CategoryName { get; set; }
        public string HomeSideDescription { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
        public int ForTake { get; set; }
        public int ForTakeoutDetail { get; set; }
        public int Count { get; set; }
        public List<SalesProduct> salesProducts { get; set; }
        public string ImageUrlTakeOutSide { get; set; }
    }
}
