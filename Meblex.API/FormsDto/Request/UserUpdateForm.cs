using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class UserUpdateForm
    {
        public string Name { get; set; }

        public string NIP { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }
    }

    public class UserUpdateFormValidation : AbstractValidator<UserUpdateForm>
    {
        public UserUpdateFormValidation()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(32).When(x => x.Name != null);
            RuleFor(x => x.NIP).NotNull().NotEmpty().Matches("^[0-9]*$").Length(10).When(x => x.NIP != null);
            RuleFor(x => x.Address).NotNull().NotEmpty().MaximumLength(32).When(x => x.Address != null);
            RuleFor(x => x.State).NotNull().NotEmpty().MaximumLength(32).When(x => x.State != null);
            RuleFor(x => x.City).NotNull().NotEmpty().MaximumLength(32).When(x => x.City != null);
            RuleFor(x => x.PostCode).NotNull().NotEmpty().Matches(@"\b\d{5}\b/g").When(x => x.PostCode != null);
        }
    }
}
