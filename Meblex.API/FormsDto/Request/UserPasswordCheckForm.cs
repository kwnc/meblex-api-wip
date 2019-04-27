using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class UserPasswordCheckForm
    {
        public string Password { get; set; }
    }

    public class UserPasswordCheckFormValidation : AbstractValidator<UserPasswordCheckForm>
    {
        public UserPasswordCheckFormValidation()
        {
            RuleFor(x => x.Password).NotEmpty().NotNull()
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$");
        }
    }
}
