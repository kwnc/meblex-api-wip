using Meblex.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Meblex.API.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FitterController : ControllerBase
    {
        public readonly IFitterService _fitterService;
        private readonly IStringLocalizer<FitterController> _localizer;
        public FitterController(IFitterService fitterService, IStringLocalizer<FitterController> localizer)
        {
            _fitterService = fitterService;
            _localizer = localizer;
        }
    }
}