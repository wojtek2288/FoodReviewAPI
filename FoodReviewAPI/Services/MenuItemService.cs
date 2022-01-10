using AutoMapper;
using FoodReviewAPI.Data;
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
    }
}
