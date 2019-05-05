using System.Threading.Tasks;
using AutoMapper;
using Meblex.API.DTO;
using Meblex.API.FormsDto.Request;
using Meblex.API.FormsDto.Response;
using Meblex.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Mapper = AgileObjects.AgileMapper.Mapper;

namespace Meblex.API.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly IUserService _userService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        public UserController(IJWTService jwtService, IUserService userService, IClientService clientService, IMapper mapper)
        {
            _jwtService = jwtService;
            _userService = userService;
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpPut("update")]
        [SwaggerOperation(
            Summary = "Update client data",
            Description = "Update user data from request body",
            OperationId = "UserUpdate")]
        [SwaggerResponse(200,"Successful client data update", typeof(ClientUpdateResponse))]
        [SwaggerResponse(500)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> UpdateUserData([FromBody] UserUpdateForm userUpdateForm)
        {
            var id = _jwtService.GetAccessTokenUserId(User);
            var clientId = await _clientService.GetClientIdFromUserId(id);
//            var client = _mapper.Map<ClientUpdateDto>(userUpdateForm);
            var client = Mapper.Map(userUpdateForm).ToANew<ClientUpdateDto>();
            

            var isUpdated = await _clientService.UpdateClientData(client, clientId);

            if (!isUpdated)
            {
                return StatusCode(500);
            }

            var clientData = await _clientService.GetClientData(clientId);

            return StatusCode(200, clientData);
        }

        [HttpPut("email")]
        [SwaggerOperation(
            Summary = "Update client email",
            Description = "Update user email from request body",
            OperationId = "UserUpdateEmail")]
        [SwaggerResponse(204)]
        [SwaggerResponse(500)]
        [SwaggerResponse(409)]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UserEmailUpdateForm userEmailUpdateForm )
        {
            var id = _jwtService.GetAccessTokenUserId(User);
//            var isMatching = await _userService.CheckIfEmailIsMatching(id, userEmailUpdateForm.OldEmail);
            var exist = await _userService.CheckIfUserWithEmailExist(userEmailUpdateForm.NewEmail);

            if ( !exist) return StatusCode(409);

            var isChanged = await _userService.UpdateUserEmail(id, userEmailUpdateForm.NewEmail);

            return isChanged ? StatusCode(204) : StatusCode(500);

        }

        [HttpPut("password")]
        [SwaggerOperation(
            Summary = "Update client password",
            Description = "Update user password from request body",
            OperationId = "UserUpdatePassword")]
        [SwaggerResponse(204)]
        [SwaggerResponse(500)]
        [SwaggerResponse(409)]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UserPasswordUpdateForm userPasswordUpdateForm)
        {
            var id = _jwtService.GetAccessTokenUserId(User);
            var isMatching = await _userService.CheckIfPasswordIsMatching(id, userPasswordUpdateForm.OldPassword);

            if (!isMatching) return StatusCode(409);

            var isChanged = await _userService.UpdateUserPassword(id, userPasswordUpdateForm.NewPassword);

            return isChanged ? StatusCode(204) : StatusCode(500);

        }

        [HttpGet("check/password")]
        [SwaggerOperation(
            Summary = "Check user password",
            Description = "Check if user has the same password already",
            OperationId = "UserCheckPassword")]
        [SwaggerResponse(204)]
        [SwaggerResponse(500)]
        [SwaggerResponse(409)]
        public async Task<IActionResult> CheckUserPassword([FromBody] UserPasswordCheckForm userPasswordCheckForm )
        {
            var id = _jwtService.GetAccessTokenUserId(User);
            var isMatching = await _userService.CheckIfPasswordIsMatching(id, userPasswordCheckForm.Password);

            return isMatching ? StatusCode(204): StatusCode(409);
        }

        [HttpGet("check/email")]
        [SwaggerOperation(
            Summary = "Update user email",
            Description = "Check if user has the same email already or if exist in db",
            OperationId = "UserCheckEmail")]
        [SwaggerResponse(204)]
        [SwaggerResponse(500)]
        [SwaggerResponse(409)]
        public async Task<IActionResult> CheckUserEmail([FromBody] UserEmailCheckForm userEmailCheckForm)
        {
            var id = _jwtService.GetAccessTokenUserId(User);
            var isMatching = await _userService.CheckIfEmailIsMatching(id, userEmailCheckForm.Email);

            return isMatching ? StatusCode(204) : StatusCode(409);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get client data",
            Description = "Receiving all client data",
            OperationId = "UserGetData")]
        [SwaggerResponse(500)]
        [SwaggerResponse(200, null,typeof(ClientUpdateResponse))]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetUserData()
        {
            var id = _jwtService.GetAccessTokenUserId(User);
            var userData = await _clientService.GetClientData(id);

            return StatusCode(200, userData);
        }

    }
}
