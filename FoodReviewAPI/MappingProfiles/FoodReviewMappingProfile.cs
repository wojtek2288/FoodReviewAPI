using AutoMapper;
using FoodReviewAPI.Entities;
using FoodReviewAPI.Models;
using System.Collections.Generic;

namespace FoodReviewAPI.MappingProfiles
{
    public class FoodReviewMappingProfile : Profile
    {
        public FoodReviewMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.Category, c => c.MapFrom(c => c.Category.Name))
                .ForMember(m => m.MenuItems, c => c.MapFrom(c => c.MenuItems));

            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(m => m.Reviews, c => c.MapFrom(c => c.Reviews));

            CreateMap<Review, ReviewDto>();

            CreateMap<CreateUpdateRestaurantDto, Restaurant>()
                .ForMember(m => m.Category, c => c.MapFrom(c => new Category()
                {
                    Name = c.Name,
                }))
                .ForMember(m => m.MenuItems, c => c.MapFrom(c => new List<MenuItem>()));
        }
            
    }
}
