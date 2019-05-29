using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class PartAddForm
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public float Price { get; set; }

        public int PieceOfFurnitureId { get; set; }

        public int PatternId { get; set; }

        public int ColorId { get; set; }

        public int MaterialId { get; set; }
    }

    public class PartAddFormValidator : AbstractValidator<PartAddForm>
    {
        public PartAddFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x => x.PieceOfFurnitureId).NotEmpty().NotNull().GreaterThan(-1);
            RuleFor(x => x.MaterialId).NotEmpty().NotNull().GreaterThan(-1);
            RuleFor(x => x.PatternId).NotEmpty().NotNull().GreaterThan(-1);
            RuleFor(x => x.ColorId).NotEmpty().NotNull().GreaterThan(-1);
            RuleFor(x => x.Count).NotEmpty().NotNull().GreaterThan(-1);
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThan(-1);
        }
    }
}
