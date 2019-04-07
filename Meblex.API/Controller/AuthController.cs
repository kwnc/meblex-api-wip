using System.Threading.Tasks;
using Meblex.API.DTO;
using Meblex.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meblex.API.Controller
{

//    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

//        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Auth([FromBody] UserLoginForm user)
        {
            var userToken = await _authService.AuthUser(user.Login, user.Password);
            return  userToken == null ? (IActionResult) StatusCode(401) : Ok(userToken);
        }

//        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterForm registerForm)
        {
            var registedUserInfo = await _authService.RegisterNewUser(registerForm);
            if (registedUserInfo == null) return StatusCode(500);
            var userToken = await  _authService.AuthUser(registerForm.Email, registerForm.Password);

            registedUserInfo.Token = userToken.Token;
            return userToken == null ? (IActionResult) StatusCode(500) : StatusCode(201, registedUserInfo);
        }
    }
}