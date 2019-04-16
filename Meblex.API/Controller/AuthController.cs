using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Meblex.API.DTO;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Meblex.API.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meblex.API.Controller
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly IJWTService _jwtService;
        public AuthController(IAuthService authService, IJWTService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Auth([FromBody] UserLoginForm user)
        {
            var accessToken = await _authService.GetAccessToken(user.Login, user.Password);
            var refreshToken = await _authService.GetRefreshToken(user.Login, user.Password);

            return  accessToken != null && refreshToken != null ? (IActionResult) StatusCode(200, new TokenResponse() { AccessToken = accessToken, RefreshToken = refreshToken }) : StatusCode(500);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterForm registerForm)
        {
            var registedUserInfo = await _authService.RegisterNewUser(registerForm);
            if (registedUserInfo == null) return StatusCode(500);
            var accessToken = await  _authService.GetAccessToken(registerForm.Email, registerForm.Password);
            var refreshToken = await _authService.GetRefreshToken(registerForm.Email, registerForm.Password);

            return refreshToken != null && accessToken != null ? (IActionResult)StatusCode(201, new TokenResponse() { AccessToken = accessToken, RefreshToken = refreshToken }) : StatusCode(500);
        }

        [AllowAnonymous]
        [HttpPut("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenForm token)
        {
            try
            {
                var userId = _jwtService.GetRefreshTokenUserId(token.Token);
                var accessToken = await _authService.GetAccessToken(userId);
                var refreshToken = await _authService.GetRefreshToken(userId);
                return refreshToken != null && accessToken != null ? (IActionResult)StatusCode(201, new TokenResponse(){AccessToken = accessToken, RefreshToken = refreshToken}) : StatusCode(500);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}