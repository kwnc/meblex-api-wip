using System.Threading.Tasks;
using Meblex.API.Context;
using Meblex.API.DTO;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Meblex.API.Interfaces
{
    public interface IAuthService
    {
        Task<UserToken> AuthUser(string login, string password);
        Task<UserConfirmedRegistation> RegisterNewUser(UserRegisterForm userRegisterForm);
    }
}