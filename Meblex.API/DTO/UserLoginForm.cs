using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Meblex.API.DTO
{
    public class UserLoginForm
    {
        public string Login { get; set; }
        
        public string Password { get; set; }
    }

    public class UserLoginFormValidator:AbstractValidator<UserLoginForm>{
        public UserLoginFormValidator()
        {
            RuleFor(x => x.Login).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty()
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$");
        }
    }
}