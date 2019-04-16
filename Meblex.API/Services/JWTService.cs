using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Meblex.API.Services
{
    public class JWTService:IJWTService
    {
        private readonly JWTSettings _jwtSettings;
        public JWTService(JWTSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GetClaimValue(string claim, TokenValidationParameters tokenValidationParameters, string token)
        {
            var claimsPrincipal = new JwtSecurityTokenHandler()
                .ValidateToken(token, tokenValidationParameters, out var rawValidatedToken);




            return claimsPrincipal.FindFirst(claim).Value;
        }

        public int GetAccessTokenUserId(string token)
        {
            return int.Parse(GetClaimValue(ClaimTypes.Name,
                _jwtSettings.GetTokenValidationParameters(_jwtSettings.AccessTokenSecret), token));
        }

        public int GetRefreshTokenUserId(string token)
        {
            return int.Parse(GetClaimValue(ClaimTypes.Name,
                _jwtSettings.GetTokenValidationParameters(_jwtSettings.RefreshTokenSecret), token));
        }

       
    }
}
