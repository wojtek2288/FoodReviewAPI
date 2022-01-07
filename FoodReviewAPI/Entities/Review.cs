using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReviewAPI.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }
        public double Rating { get; set; }
    }
}
