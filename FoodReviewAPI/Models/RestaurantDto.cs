using FoodReviewAPI.Entities;
using System.Collections.Generic;

namespace FoodReviewAPI.Models
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double? RestaurantReview { get; set; }
        public List<MenuItemDto> MenuItems { get; set; }
    }
}
