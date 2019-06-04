using System;
using System.Net;
using Meblex.API.Helper;
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
        [SwaggerOperation(
            Summary = "Test endpoint",
            Description = "User can check if everything is OK",
            OperationId = "TestCheck")]
        [SwaggerResponse(200,"Health Check", typeof(string))]
        [SwaggerResponse(401)]
        [SwaggerResponse(500)]
        public IActionResult Index()
        {

            return Ok("Everything works");

        }

        [HttpGet("ping")]
        [SwaggerOperation(
            Summary = "Ping Pong Endpoint",
            Description = "User can ping server",
            OperationId = "TestPing")]
        [SwaggerResponse(200, "Ping", typeof(string))]
        [SwaggerResponse(401)]
        [SwaggerResponse(500)]
        public IActionResult Pong()
        {
            return Ok("Pong");
        }

    }

}