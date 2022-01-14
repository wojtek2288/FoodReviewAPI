using FoodReviewAPI.Models;
using FoodReviewAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FoodReviewAPI.Controllers
{
    [ApiController]
    [Route("api/restaurant/{restaurantId}/menuItem")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MenuItemDto>> GetMenuItems([FromRoute]int restaurantId)
        {
            var menuItems = _menuItemService.GetRestaurantMenuItems(restaurantId);

            return Ok(menuItems);
        }

        [HttpGet("{menuItemId}")]
        public ActionResult<MenuItemDto> GetMenuItem([FromRoute]int restaurantId, [FromRoute]int menuItemId)
        {
            var menuItem = _menuItemService.GetRestaurantMenuItemById(restaurantId, menuItemId);

            return Ok(menuItem);
        }

        [HttpPost]
        public ActionResult CreateMenuItem([FromRoute]int restaurantId, [FromBody]CreateUpdateMenuItemDto dto)
        {
            int id = _menuItemService.Create(restaurantId, dto);
            return Created($"api/restaurant/{restaurantId}/menuItem/{id}", null);
        }

        [HttpDelete("{menuItemId}")]
        public ActionResult DeleteMenuItem([FromRoute]int menuItemId)
        {
            _menuItemService.Delete(menuItemId);

            return Ok();
        }

        [HttpPut("{menuItemId}")]
        public ActionResult UpdateMenuItem([FromRoute]int menuItemId, [FromBody]CreateUpdateMenuItemDto dto)
        {
            _menuItemService.Update(menuItemId, dto);
            return Ok();
        }
    }
}
