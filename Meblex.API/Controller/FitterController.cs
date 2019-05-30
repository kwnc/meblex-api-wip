using Meblex.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meblex.API.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FitterController : ControllerBase
    {
        public readonly IFitterService _fitterService;
        public FitterController(IFitterService fitterService)
        {
            _fitterService = fitterService;
        }


    }
}