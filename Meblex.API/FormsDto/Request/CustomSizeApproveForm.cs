using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class CustomSizeApproveForm
    {
        public int CustomSizeFormId { get; set; }

        public float Price { get; set; }
    }

    public class CustomSizeApproveFormValidator : AbstractValidator<CustomSizeApproveForm>
    {
        public CustomSizeApproveFormValidator()
        {
            RuleFor(x => x.CustomSizeFormId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
