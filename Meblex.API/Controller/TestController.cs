using Microsoft.AspNetCore.Mvc;

namespace Meblex.API.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController :ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            
            return StatusCode(200, new Test(){Name = "dupa"});

        }
    }
    public class Test
    {
        public string Name { get; set; }
    }
}