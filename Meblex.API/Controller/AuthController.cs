using System.Threading.Tasks;
using AutoMapper;
using Meblex.API.DTO;
using Meblex.API.FormsDto.Response;
using Meblex.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Swagger;

namespace Meblex.API.Controller
{

    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly IJWTService _jwtService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IJWTService jwtService, IMapper mapper, IUserService userService)
        {
            _authService = authService;
            _jwtService = jwtService;
            _mapper = mapper;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerResponse(200,"",typeof(AuthLoginResponse))]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Login([FromBody] UserLoginForm user)
        {
            var accessToken = await _authService.GetAccessToken(user.Login, user.Password);
            var refreshToken = await _authService.GetRefreshToken(user.Login, user.Password);
            var userData = await _userService.GetUserData(user.Login);

            
            var response = _mapper.Map<AuthLoginResponse>(userData);
            response.AccessToken = accessToken;
            response.RefreshToken = refreshToken;
            response.Email = user.Login;
            
            

            return  accessToken != null && refreshToken != null && userData != null ? 
                (IActionResult) StatusCode(200, response) : StatusCode(500);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [SwaggerResponse(201, "", typeof(TokenResponse))]
        [SwaggerResponse(500)]
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
        [SwaggerResponse(201, "", typeof(TokenResponse))]
        [SwaggerResponse(500)]
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