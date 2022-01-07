using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReviewAPI.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual List<Review> Reviews { get; set; }
    }
}
