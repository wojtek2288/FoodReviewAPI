using System.ComponentModel.DataAnnotations;

namespace FoodReviewAPI.Models
{
    public class CreateUpdateRestaurantDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [MaxLength(25)]
        public string Category { get; set; }
    }
}
