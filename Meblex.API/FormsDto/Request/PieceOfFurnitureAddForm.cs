using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Meblex.API.Services;
using Meblex.API.Validation;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Meblex.API.FormsDto.Request
{
    public class PieceOfFurnitureAddForm
    {
        public PieceOfFurnitureAddForm()
        {
            PartsId = new List<int>();
        }
        public string Name { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public string Size { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
        public int MaterialId { get; set; }
        public int PatternId { get; set; }
        public int ColorId { get; set; }
        public int RoomId { get; set; }
        public List<string> Photos { get; set; }

        public List<int> PartsId { get; set; }
    }

    public class PieceOfFurnitureAddFormValidator : AbstractValidator<PieceOfFurnitureAddForm>
    {
        public PieceOfFurnitureAddFormValidator()
        {
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.PatternId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ColorId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.MaterialId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.RoomId).NotNull().NotEmpty().GreaterThan(0);
            RuleForEach(x => x.PartsId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(128);
            RuleFor(x => x.Count).NotNull().NotEmpty().GreaterThan(-1);
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThan(-1);
            RuleFor(x => x.Size).NotEmpty().NotNull().Matches(@"(\d+)x(\d+)x(\d+)");
            RuleForEach(x => x.Photos).NotNull().NotEmpty();


        }
    } 
}
