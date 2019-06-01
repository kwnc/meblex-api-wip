using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class CustomSizeAddFrom
    {
        public string Size { get; set; }

        public int PieceOfFurnitureId { get; set; }
    }

    public class CustomSizeAddFormValidator : AbstractValidator<CustomSizeAddFrom>
    {
        public CustomSizeAddFormValidator()
        {
            RuleFor(x => x.PieceOfFurnitureId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Size).NotEmpty().NotNull().Matches(@"(\d+)x(\d+)x(\d+)");

        }
    }
}
