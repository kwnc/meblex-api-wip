using System.ComponentModel.DataAnnotations;
using System.Data;
using FluentValidation;

namespace Meblex.API.DTO
{
    
    public class UserRegisterForm
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string NIP { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }
    }
    public class UserRegisterFormValidator : AbstractValidator<UserRegisterForm>
    {
        public UserRegisterFormValidator()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress().NotEmpty();
            RuleFor(x => x.Password).NotEmpty().NotNull()
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$");
            RuleFor(x => x.Name).NotNull().MaximumLength(32);
            RuleFor(x => x.NIP).NotNull().Matches("^[0-9]*$").Length(10).When(x => !string.IsNullOrEmpty(x.Name) || !string.IsNullOrWhiteSpace(x.Name)); 
            RuleFor(x => x.Address).NotNull().MaximumLength(32);
            RuleFor(x => x.State).NotNull().MaximumLength(32);
            RuleFor(x => x.City).NotNull().MaximumLength(32);
            RuleFor(x => x.PostCode).NotNull().Matches("\\b\\d{5}\\b/g");

        }
    }
}
