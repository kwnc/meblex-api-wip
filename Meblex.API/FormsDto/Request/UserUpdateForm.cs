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
            RuleFor(x => x.Name).MaximumLength(32).When(x => !string.IsNullOrEmpty(x.Name));
            RuleFor(x => x.NIP).Matches("^[0-9]*$").Length(10).When(x => !string.IsNullOrEmpty(x.NIP) || !string.IsNullOrWhiteSpace(x.NIP));
            RuleFor(x => x.Address).MaximumLength(32).When(x => !string.IsNullOrEmpty(x.Address));
            RuleFor(x => x.State).MaximumLength(32).When(x => !string.IsNullOrEmpty(x.State));
            RuleFor(x => x.City).MaximumLength(32).When(x => !string.IsNullOrEmpty(x.City));
            RuleFor(x => x.PostCode).Matches(@"\b\d{5}\b").When(x => !string.IsNullOrEmpty(x.PostCode));
        }
    }
}
