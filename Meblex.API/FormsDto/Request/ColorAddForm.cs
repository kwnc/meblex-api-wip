using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class ColorAddForm
    {
        public string Name { get; set; }

        public string HexCode { get; set; }

        public string Slug { get; set; }
    }

    public class ColorAddFormValidator : AbstractValidator<ColorAddForm>
    {
        public ColorAddFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x => x.HexCode).NotEmpty().NotNull().Matches(@"^#(?:[0-9a-fA-F]{3}){1,2}$");
            RuleFor(x => x.Slug).NotEmpty().NotNull().MaximumLength(128);
        }
    } 
}
