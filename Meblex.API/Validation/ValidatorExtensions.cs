using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Meblex.API.Context;
using Meblex.API.Helper;
using Meblex.API.Interfaces;

namespace Meblex.API.Validation
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderInitial<T, string> MustBeRefreshToken<T>(this IRuleBuilder<T, string> ruleBuilder)
        {

            return ruleBuilder.Custom((token, contextPropertyValidatorContext) =>
            {


                var context = contextPropertyValidatorContext.ParentContext.GetServiceProvider();
                var jwtSettings = (JWTSettings)context.GetService(typeof(JWTSettings));

                var meblexDbContext = (MeblexDbContext)context.GetService(typeof(MeblexDbContext));

                var tokenValidationParameters = jwtSettings.GetTokenValidationParameters(jwtSettings.RefreshTokenSecret);

                var jwtService = (IJWTService)context.GetService(typeof(IJWTService));

                try
                {

                    var userId = jwtService.GetRefreshTokenUserId(token);

                    var userExist = meblexDbContext.Users.FirstOrDefault(x => x.UserId == userId);

                    if (userExist == null) contextPropertyValidatorContext.AddFailure(" 'token' is not connected with user");

                    var expTicks = int.Parse(jwtService.GetClaimValue("exp", tokenValidationParameters, token));
                    var expDate = DateTimeOffset.UnixEpoch.AddSeconds(expTicks);

                    if (! (expDate > DateTime.UtcNow))
                    {
                        contextPropertyValidatorContext.AddFailure("'token' is expired");
                    }
                }
                catch
                {
                    contextPropertyValidatorContext.AddFailure("'token' is not valid");
                }
            });
        }

        public static IRuleBuilderInitial<T, string> IsImage<T>(this IRuleBuilder<T, string> ruleBuilder)
        {

            return ruleBuilder.Custom((photo, contextPropertyValidatorContext) =>
            {

                var bytes = Convert.FromBase64String(photo.Split(',')[1]);

                using (var stream = new MemoryStream(bytes))
                {
                    try
                    {
                        var image = Image.FromStream(stream);
                        image.Dispose();
                    }
                    catch
                    { 
                        contextPropertyValidatorContext.AddFailure("Not valid photo");
                    }
                }

            });
        }
    }
}
