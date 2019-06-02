using System.Collections.Generic;
using Meblex.API.FormsDto.Request;
using Meblex.API.FormsDto.Response;
using Meblex.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meblex.API.Controller
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShoppingCartController:ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IJWTService _jwtService;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IJWTService jwtService)
        {
            _shoppingCartService = shoppingCartService;
            _jwtService = jwtService;
        }

        [Authorize(Roles = "Client")]
        [HttpPost("make")]
        public IActionResult AddOrder([FromBody] OrderAddForm order)
        {
            var userId = _jwtService.GetAccessTokenUserId(User);
            var id = _shoppingCartService.AddOrder(userId, order);
            var response = _shoppingCartService.GetClientById(id, userId);

            return StatusCode(201, response);
        }
        [Authorize(Roles = "Client")]
        [HttpGet("client/list")]
        public IActionResult GetAllClientOrders()
        {
            var userId = _jwtService.GetAccessTokenUserId(User);
            var response = _shoppingCartService.GetAllClientOrders(userId);
            return response.Count == 0 ? StatusCode(204, new List<OrderResponse>()) : StatusCode(200, response);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("client/list/{id}")]
        public IActionResult GetOneClientOrder(int id)
        {
            var userId = _jwtService.GetAccessTokenUserId(User);
            var response = _shoppingCartService.GetClientById(id, userId);
            return StatusCode(200, response);
        }
    }
}
