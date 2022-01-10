﻿using FoodReviewAPI.Entities;
using System.Collections.Generic;

namespace FoodReviewAPI.Models
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}
