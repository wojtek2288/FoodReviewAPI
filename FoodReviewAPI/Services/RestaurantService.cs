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
    public interface IRestaurantService
    {
        public IEnumerable<RestaurantDto> GetAll();
        public int Create(CreateUpdateRestaurantDto dto);
        public void Delete(int restaurantId);
        public RestaurantDto GetById(int id);
        public void Update(int id, CreateUpdateRestaurantDto dto);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(DatabaseContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.MenuItems)
                .Include(r => r.Category);

            var result = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return result;
        }

        public int Create(CreateUpdateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);

            if(_dbContext.Categories.FirstOrDefault(e => e.Name == dto.Category) is null)
            {
                _dbContext.Categories.Add(new Category()
                {
                    Name = dto.Category,
                });
            }

            if (_dbContext.SaveChanges() == 0)
                throw new InternalServerErrorException("Couldn't save restaurant or category");

            return restaurant.Id;
        }

        public void Delete(int restaurantId)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == restaurant.CategoryId);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var restaurants =
                from r in _dbContext.Restaurants
                join c in _dbContext.Categories on r.CategoryId equals c.Id
                where c.Id == restaurant.CategoryId
                select r;

            if(restaurant.MenuItems != null)
                foreach(var menuItem in restaurant.MenuItems)
                    _dbContext.MenuItems.Remove(menuItem);

            if (restaurants.Count() == 1)
                _dbContext.Categories.Remove(category);

            _dbContext.Restaurants.Remove(restaurant);

            if(_dbContext.SaveChanges() == 0)
                throw new InternalServerErrorException("Couldn't delete restaurant");

            return;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext.Restaurants
                .Include(r => r.MenuItems)
                .Include(r => r.Category)
                .FirstOrDefault(r => r.Id == id);   

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var result = _mapper.Map<RestaurantDto>(restaurant);

            return result;
        }

        public void Update(int id, CreateUpdateRestaurantDto dto)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == id);
            var category = _dbContext.Categories.FirstOrDefault(c => c.Name == dto.Category);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            if (category is null)
                _dbContext.Categories.Add(new Category() { Name = dto.Category});

            var foundCategory = _dbContext.Categories.FirstOrDefault(c => c.Name == dto.Category);

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.Category = foundCategory;
            restaurant.CategoryId = foundCategory.Id;

            _dbContext.Restaurants.Update(restaurant);
            _dbContext.SaveChanges();

            return;
        }
    }
}
