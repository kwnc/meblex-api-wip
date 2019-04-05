using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meblex.API.Controller
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController :ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {

            return Ok("Everything works");

        }

        [HttpGet("ping")]
        public IActionResult Pong()
        {
            return Ok("Pong");
        }
    }

}