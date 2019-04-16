using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Meblex.API.Context;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meblex.API.Validation
{
    public class RefreshTokenValidation : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var token = (string) value;

            var jwtSettings = (JWTSettings) validationContext.GetService(typeof(JWTSettings));

            var context = (MeblexDbContext) validationContext.GetService(typeof(MeblexDbContext));

            var tokenValidationParameters = jwtSettings.GetTokenValidationParameters(jwtSettings.RefreshTokenSecret);

            var jwtService = (IJWTService) validationContext.GetService(typeof(IJWTService));

            try
            {

                var userId = jwtService.GetRefreshTokenUserId(token);

                var userExist =  context.Users.FirstOrDefault(x => x.UserId == userId);

                if (userExist == null) return new ValidationResult("Token user not found");

                var expTicks = int.Parse(jwtService.GetClaimValue("exp", tokenValidationParameters, token));
                var expDate = DateTimeOffset.UnixEpoch.AddSeconds(expTicks);

                return expDate > DateTime.UtcNow ? ValidationResult.Success : new ValidationResult("Expired refresh token");
            }
            catch
            {
                return new ValidationResult("Refresh token not valid");
            }
        }
    }
}
