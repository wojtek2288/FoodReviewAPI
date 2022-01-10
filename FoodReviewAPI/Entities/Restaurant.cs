using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReviewAPI.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public virtual Category Category { get; set; }
        public double? RestaurantReview { get; set; }
        public virtual List<MenuItem> MenuItems { get; set; }
    }
}
