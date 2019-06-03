using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Meblex.API.Validation;

namespace Meblex.API.FormsDto.Request
{
    public class TmpAddForm
    {
        public int PieceOfFurnitureId { get; set; }
        public List<string> Photos { get; set; }
    }

    public class TmpAddFormValidator : AbstractValidator<TmpAddForm>
    {
        public TmpAddFormValidator()
        {
            RuleFor(x => x.PieceOfFurnitureId).NotEmpty().NotNull().GreaterThan(0);
            RuleForEach(x => x.Photos).NotEmpty().NotNull().IsImage();
        }
    }
}
