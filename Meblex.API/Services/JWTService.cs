using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Dawn;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;

namespace Meblex.API.Services
{
    public class JWTService:IJWTService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly IStringLocalizer<JWTService> _localizer;
        public JWTService(JWTSettings jwtSettings, IStringLocalizer<JWTService> localizer)
        {
            _jwtSettings = jwtSettings;
            _localizer = localizer;
        }

        public string GetClaimValue(string claim, TokenValidationParameters tokenValidationParameters, string token)
        {
            var Token = Guard.Argument(token, nameof(token)).NotEmpty().NotNull().NotWhiteSpace();
            var Claim = Guard.Argument(claim, nameof(claim)).NotEmpty().NotNull().NotWhiteSpace();
            var TokenValidationParameters = Guard.Argument(tokenValidationParameters, nameof(tokenValidationParameters))
                .NotNull().Value;
            var claimsPrincipal = new JwtSecurityTokenHandler()
                .ValidateToken(Token, TokenValidationParameters, out var rawValidatedToken);

            return claimsPrincipal.FindFirst(Claim).Value;
        }

        public int GetAccessTokenUserId(string token)
        {
            var Token = Guard.Argument(token, nameof(token)).NotEmpty().NotNull().NotWhiteSpace();

            return int.Parse(GetClaimValue(ClaimTypes.Name,
                _jwtSettings.GetTokenValidationParameters(_jwtSettings.AccessTokenSecret), Token));
        }

        public int GetRefreshTokenUserId(string token)
        {
            var Token = Guard.Argument(token, nameof(token)).NotEmpty().NotNull().NotWhiteSpace();
            return int.Parse(GetClaimValue(ClaimTypes.Name,
                _jwtSettings.GetTokenValidationParameters(_jwtSettings.RefreshTokenSecret),Token));
        }

        public int GetAccessTokenUserId(ClaimsPrincipal principal)
        {
            var Principal = Guard.Argument(principal, nameof(principal)).NotNull().Value;

            return int.Parse(Principal.FindFirst(ClaimTypes.Name).Value);
        }

       
    }
}
