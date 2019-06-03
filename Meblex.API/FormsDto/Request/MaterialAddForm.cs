using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Meblex.API.Validation;

namespace Meblex.API.FormsDto.Request
{
    public class MaterialAddForm
    {
        public string Name { get; set; }

        public string Slug { get; set; }

        public string Photo { get; set; }
    }

    public class MaterialAddFormValidator : AbstractValidator<MaterialAddForm>
    {
        public MaterialAddFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x => x.Slug).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x => x.Photo).NotEmpty().NotNull().IsImage();
        }
    }
}
