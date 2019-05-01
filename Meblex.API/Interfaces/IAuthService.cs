using System.Threading.Tasks;
using Meblex.API.Context;
using Meblex.API.DTO;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Meblex.API.Interfaces
{
    public interface IAuthService
    {

        Task<UserConfirmedRegistation> RegisterNewUser(AuthRegisterForm authRegisterForm);
        Task<string> GetAccessToken(string login, string password);
        Task<string> GetRefreshToken(string login, string password);
        Task<string> GetAccessToken(int id);
        Task<string> GetRefreshToken(int id);
        Task<bool> CheckUser(string email, string password)
        string PasswordHasher(string password);
    }
}