using FoodReviewAPI.Data;
using FoodReviewAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FoodReviewAPI
{
    public class FoodReviewSeeder
    {
        private readonly DatabaseContext _dbContext;
        
        public FoodReviewSeeder(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;    
        }

        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }

                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if(!_dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    _dbContext.AddRange(categories);
                    _dbContext.SaveChanges();
                }

                if(!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role() { Name = "User"},
                new Role() { Name = "Admin"},
                new Role() { Name = "VerifiedReviewer"}
            };

            return roles;
        }

        private IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category() { Name = "Fast Food"},
                new Category() { Name = "Cafe"},
                new Category() { Name = "Fine Dining"}
            };

            return categories;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name =  "McDonalds",
                    Description = "American fast food company, founded in 1940. McDonald's is the world's largest restaurant chain by revenue, " +
                    "serving over 69 million customers daily in over 100 countries",
                    Category = _dbContext.Categories.FirstOrDefault(e => e.Name == "Fast Food"),
                    RestaurantReview = null,
                    MenuItems = new List<MenuItem>()
                    {
                        new MenuItem()
                        {
                            Name = "Big Mac",
                            Description = "The Big Mac contains two beef patties, sauce, lettuce, cheese, pickles, and onions on a " +
                            "three-piece sesame seed bun.",
                            Price = 13.50m,
                            Reviews = new List<Review>()
                        },
                        new MenuItem()
                        {
                            Name = "Cheeseburger",
                            Description = "Classic McDonalds cheeseburger, containing one beef patty, cheese and pickles.",
                            Price = 4.50m,
                            Reviews = new List<Review>()
                        }
                    }
                },
                new Restaurant()
                {
                    Name =  "KFC",
                    Description = "American fast food restaurant chain headquartered in Louisville, Kentucky that specializes in fried chicken",
                    Category = _dbContext.Categories.FirstOrDefault(e => e.Name == "Fast Food"),
                    RestaurantReview = null,
                    MenuItems = new List<MenuItem>()
                    {
                        new MenuItem()
                        {
                            Name = "Zinger",
                            Description = "Classic chicken sandwitch with mayo sauce.",
                            Price = 9.50m,
                            Reviews = new List<Review>()
                        }
                    }
                },
            };

            return restaurants;
        }
    }
}
