using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dawn;
using Meblex.API.FormsDto.Request;
using Meblex.API.FormsDto.Response;
using Meblex.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Meblex.API.Controller
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomSizeController : ControllerBase
    {
        public readonly IJWTService _jwtService;
        public readonly ICustomSizeService _customSizeService;

        public CustomSizeController(IJWTService jwtService, ICustomSizeService customSizeService)
        {
            _jwtService = jwtService;
            _customSizeService = customSizeService;
        }

        [HttpPost("client/add")]
        [SwaggerResponse(500)]
        [SwaggerResponse(201, "",typeof(CustomSizeFormResponse))]
        public IActionResult AddCustomSizeForm([FromBody] CustomSizeAddFrom form)
        {
            var userId = _jwtService.GetAccessTokenUserId(User);
            var id = _customSizeService.AddCustomSize(form, userId);
            var response = _customSizeService.GetById(id);

            return StatusCode(201, response);
        }

        [Authorize(Roles = "Worker")]
        [HttpPost("accept")]
        [SwaggerResponse(500)]
        [SwaggerResponse(404)]
        [SwaggerResponse(201, "", typeof(CustomSizeFormResponse))]
        public IActionResult ApproveCustomSizeForm([FromBody] CustomSizeApproveForm form)
        {
            var response = _customSizeService.ApproveCustomSizeForm(form.CustomSizeFormId, form.Price);

            return StatusCode(201, response);
        }

        [Authorize(Roles = "Worker")]
        [HttpGet("all")]
        [SwaggerResponse(500)]
        [SwaggerResponse(204, "", typeof(List<>))]
        [SwaggerResponse(404)]
        [SwaggerResponse(200, "", typeof(List<CustomSizeFormResponse>))]
        public IActionResult GetAllCustomSizeForms()
        {
            var response = _customSizeService.GetAllCustomSizeForm();

            return response.Count == 0? StatusCode(204, new List<CustomSizeFormResponse>()): StatusCode(200, response);
        }

        [Authorize(Roles = "Worker")]
        [HttpGet("{id}")]
        [SwaggerResponse(500)]
        [SwaggerResponse(404)]
        [SwaggerResponse(200, "", typeof(CustomSizeFormResponse))]
        public IActionResult GetById(int id)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative().NotZero();
            var response = _customSizeService.GetById(Id);

            return StatusCode(200, response);
        }

        [HttpGet("client/all")]
        [SwaggerResponse(500)]
        [SwaggerResponse(204)]
        [SwaggerResponse(200, "", typeof(List<CustomSizeFormResponse>))]
        public IActionResult GetAllClientCustomSizeForms()
        {
            var userId = _jwtService.GetAccessTokenUserId(User);
            var response = _customSizeService.GetAllClientForms(userId);

            return StatusCode(200, response);
        }

        [HttpGet("client/{id}")]
        [SwaggerResponse(500)]
        [SwaggerResponse(204)]
        [SwaggerResponse(404)]
        [SwaggerResponse(200, "", typeof(CustomSizeFormResponse))]
        public IActionResult GetClientCustomSizeForm(int id)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative().NotZero().Value;
            var userId = _jwtService.GetAccessTokenUserId(User);
            var response = _customSizeService.GetClientFormById(Id, userId);

            return StatusCode(200, response);
        }
    }
}
