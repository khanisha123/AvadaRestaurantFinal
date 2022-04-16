using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string addComment { get; set; }
        public int BulletinId { get; set; }
        public string AppUserId { get; set; }
        public Bulletin bulletin { get; set; }
        public AppUser appUser { get; set; }
        public DateTime CommentDate { get; set; }

    }
}
