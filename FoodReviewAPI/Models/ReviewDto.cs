namespace FoodReviewAPI.Models
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}
