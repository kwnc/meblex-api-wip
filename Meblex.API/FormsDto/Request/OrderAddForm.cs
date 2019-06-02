using System.Collections.Generic;
using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class OrderAddForm
    {
        public string Delivery { get; set; }
        public bool Reservation { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public virtual List<OrderLineAddForm> OrderLines { get; set; }
    }

    public class OrderAddFormValidator : AbstractValidator<OrderAddForm>
    {
        public OrderAddFormValidator()
        {
            RuleFor(x => x.Address).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x => x.City).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x => x.Delivery).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x => x.PostCode).NotNull().Matches(@"\b\d{5}\b");
            RuleFor(x => x.OrderLines).NotEmpty().NotNull();
            RuleFor(x => x.Reservation).NotNull().NotEmpty();
            RuleFor(x => x.State).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x => x.Street).NotEmpty().NotNull().MaximumLength(128);
        }
    }
}
