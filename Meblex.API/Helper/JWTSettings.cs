using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Meblex.API.Helper
{
    public class JWTSettings
    {
        public JWTSettings()
        {
            AccessTokenSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
            RefreshTokenSecret = Environment.GetEnvironmentVariable("JWT_REFRESH_SECRET");
            AccessTokenExpiredHours = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRED"));
            RefreshTokenExpiredHours = int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRED"));
        }
        public TokenValidationParameters GetTokenValidationParameters(string secret)
        {
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            return tokenValidationParameters;
        }

        public string AccessTokenSecret { get; set; }
        public string RefreshTokenSecret { get; set; }
        public int AccessTokenExpiredHours { get; set; }
        public int RefreshTokenExpiredHours { get; set; }
    }
}
