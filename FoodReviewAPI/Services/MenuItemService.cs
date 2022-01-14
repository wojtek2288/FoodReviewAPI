using AutoMapper;
using FoodReviewAPI.Data;
using FoodReviewAPI.Entities;
using FoodReviewAPI.Exceptions;
using FoodReviewAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace FoodReviewAPI.Services
{
    public interface IMenuItemService
    {
        public IEnumerable<MenuItemDto> GetRestaurantMenuItems(int id);
        public MenuItemDto GetRestaurantMenuItemById(int restaurantId, int menuItemId);
        public int Create(int restaurantId, CreateUpdateMenuItemDto dto);
        public void Delete(int menuItemId);
        public void Update(int menuItemId, CreateUpdateMenuItemDto dto);
    }

    public class MenuItemService : IMenuItemService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public MenuItemService(DatabaseContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<MenuItemDto> GetRestaurantMenuItems(int restaurantId)
        {
            var restaurant = _dbContext.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var menuItems = _mapper.Map<IEnumerable<MenuItemDto>>(restaurant.MenuItems);

            return menuItems;
        }

        public MenuItemDto GetRestaurantMenuItemById(int restaurantId, int menuItemId)
        {
            var restaurant = _dbContext.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefault(r => r.Id == restaurantId);

            var menuItem = restaurant.MenuItems.FirstOrDefault(r => r.Id == menuItemId);    

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            else if (menuItem is null)
                throw new NotFoundException("MenuItem not found");

            var result = _mapper.Map<MenuItemDto>(menuItem);

            return result;
        }

        public int Create(int restaurantId, CreateUpdateMenuItemDto dto)
        {
            var restaurant = _dbContext.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var menuItem = new MenuItem()
            {
                Restaurant = restaurant,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                RestaurantId = restaurantId,
                Reviews = new List<Review>()
            };

            restaurant.MenuItems.Add(menuItem);
            _dbContext.MenuItems.Add(menuItem);
            _dbContext.Restaurants.Update(restaurant);

            _dbContext.SaveChanges();
            return menuItem.Id;
        }

        public void Delete(int menuItemId)
        {
            var menuItem = _dbContext.MenuItems
                .Include(m => m.Reviews)
                .FirstOrDefault(m => m.Id == menuItemId);

            if (menuItem is null)
                throw new NotFoundException("Menu item not found");

            foreach(var review in menuItem.Reviews)
            {
                _dbContext.Reviews.Remove(review);
            }

            _dbContext.MenuItems.Remove(menuItem);

            int removed = _dbContext.SaveChanges();

            return;
        }

        public void Update(int menuItemId, CreateUpdateMenuItemDto dto)
        {
            var menuItem = _dbContext.MenuItems.FirstOrDefault(m => m.Id == menuItemId);

            if (menuItem is null)
                throw new NotFoundException("Menu item not found");

            menuItem.Name = dto.Name;
            menuItem.Description = dto.Description;
            menuItem.Price = dto.Price;

            _dbContext.MenuItems.Update(menuItem);
            _dbContext.SaveChanges();
            return;
        }
    }
}
