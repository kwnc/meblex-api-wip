using FluentValidation;
using Meblex.API.Validation;

namespace Meblex.API.DTO
{
    public class RefreshTokenForm
    {

        public string Token { get; set; }
    }

    public class RefreshTokenFormValidation : AbstractValidator<RefreshTokenForm>
    {
        public RefreshTokenFormValidation()
        {
            RuleFor(x => x.Token).NotNull().NotEmpty().MustBeRefreshToken();
        }


    }
}
