using FluentValidation;
using Meblex.API.Validation;

namespace Meblex.API.FormsDto.Request
{
    public class PatternAddForm
    {
        public string Name { get; set; }

        public string Slug { get; set; }

        public string Photo { get; set; }
    }

    public class PatternAddFormValidator : AbstractValidator<PatternAddForm>
    {
        public PatternAddFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x => x.Slug).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x => x.Photo).NotEmpty().NotNull();
        }
    }
}
