using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class UserPasswordUpdateForm
    {
        public string NewPassword { get; set; }

        public string OldPassword { get; set; }
    }

    public class UserPasswordUpdateFormValidation : AbstractValidator<UserPasswordUpdateForm>
    {
        public UserPasswordUpdateFormValidation()
        {
            RuleFor(x => x.NewPassword).NotEmpty().NotNull()
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$");

            RuleFor(x => x.OldPassword).NotEmpty().NotNull()
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$");
        }
    }
}
