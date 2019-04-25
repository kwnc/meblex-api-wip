using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Meblex.API.Controller
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController :ControllerBase
    {
        [HttpGet]
        [SwaggerResponse(200,"Health Check", typeof(string))]
        public IActionResult Index()
        {

            return Ok("Everything works");

        }

        [HttpGet("ping")]
        [SwaggerResponse(200, "Ping", typeof(string))]
        public IActionResult Pong()
        {
            return Ok("Pong");
        }
    }

}