using Meblex.API.FormsDto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meblex.API.Controller
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShoppingCart:ControllerBase
    {
        [Authorize(Roles = "Client")]
        [HttpPost("make")]
        public IActionResult AddOrder([FromBody] OrderAddForm order)
        {

        }

        [Authorize(Roles = "Worker")]
        [HttpGet("list")]
        public IActionResult GetAllOrders()
        {

        }
        [Authorize(Roles = "Client")]
        [HttpGet("client/list")]
        public IActionResult GetAllClientOrders()
        {

        }

        [Authorize(Roles = "Client")]
        [HttpGet("client/list/{id}")]
        public IActionResult GetOneClientOrder(int id)
        {

        }

        [Authorize(Roles = "Worker")]
        [HttpGet("list/{id}")]
        public IActionResult GetOrderByUserId(int userId)
        {

        }

        [Authorize(Roles = "Client")]
        [HttpGet("client/lines")]
        public IActionResult GetAllClientOrderLines()
        {

        }

        [Authorize(Roles = "Client")]
        [HttpGet("client/line/{id}")]
        public IActionResult GetAllClientOrderLines()
        {

        }

    }
}
