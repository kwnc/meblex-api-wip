using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class UserEmailUpdateForm
    {
        public string NewEmail { get; set; }

        public string OldEmail { get; set; }
    }

    public class UserEmailUpdateFormValidation : AbstractValidator<UserEmailUpdateForm>
    {
        public UserEmailUpdateFormValidation()
        {
            RuleFor(x => x.NewEmail).NotNull().EmailAddress().NotEmpty();
            RuleFor(x => x.OldEmail).NotNull().EmailAddress().NotEmpty();
        }
    }
}
