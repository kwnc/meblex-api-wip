using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class UserEmailCheckForm
    {
        public string Email { get; set; }
    }

    public class UserEmailCheckFormValidation : AbstractValidator<UserEmailCheckForm>
    {
        public UserEmailCheckFormValidation()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress().NotEmpty();
        }
    }
}
