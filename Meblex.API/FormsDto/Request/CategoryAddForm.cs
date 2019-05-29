using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class CategoryAddForm
    {
        public string Name { get; set; }
    }

    public class CategoryAddFormValidator : AbstractValidator<CategoryAddForm>
    {
        public CategoryAddFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(128);
        }
    }
}
