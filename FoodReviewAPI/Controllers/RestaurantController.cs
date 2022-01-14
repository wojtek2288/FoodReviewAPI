using FoodReviewAPI.Entities;
using FoodReviewAPI.Models;
using FoodReviewAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;

namespace FoodReviewAPI.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService; 
        public RestaurantController(IRestaurantService restaurantServie)
        {
            _restaurantService = restaurantServie;  
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _restaurantService.GetAll();  

            return Ok(restaurants);
        }

        [HttpPost]
        public ActionResult Create([FromBody]CreateUpdateRestaurantDto dto)
        {
            int id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _restaurantService.Delete(id);

            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetById([FromRoute]int id)
        {
            var restaurant = _restaurantService.GetById(id);    

            return Ok(restaurant);  
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRestaurant([FromRoute] int id, CreateUpdateRestaurantDto dto)
        {
            _restaurantService.Update(id, dto);

            return Ok();
        }
    }
}
