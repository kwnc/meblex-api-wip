using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Meblex.API.Interfaces
{
    public interface IJWTService
    {
        string GetClaimValue(string claim, TokenValidationParameters tokenValidationParameters, string token);
        int GetAccessTokenUserId(string token);
        int GetRefreshTokenUserId(string token);

        int GetAccessTokenUserId(ClaimsPrincipal principal);
    }
}