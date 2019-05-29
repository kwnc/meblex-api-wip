using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class RoomAddForm
    {
        public string Name { get; set; }
    }

    public class RoomAddFormValidator : AbstractValidator<RoomAddForm>
    {
        public RoomAddFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(128);
        }
    }
}
