using FluentValidation;

namespace Meblex.API.FormsDto.Request
{
    public class OrderLineAddForm
    {
        public int Count { get; set; }

        public int Price { get; set; }

        public string Size { get; set; }

        public int? PieceOfFurnitureId { get; set; }
        
        public int? PartId { get; set; }
    }

    public class OrderLineAddFormValidator : AbstractValidator<OrderLineAddForm>
    {
        public OrderLineAddFormValidator()
        {
            RuleFor(x => x.Count).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Size).NotEmpty().NotNull().Matches(@"(\d+)x(\d+)x(\d+)");
            RuleFor(x => x.PartId).Must(z => z != null && z.Value > 0).When(k => k.PieceOfFurnitureId == null);
            RuleFor(x => x.PieceOfFurnitureId).Must(z => z != null && z.Value > 0).When(k => k.PartId == null);
        }
    }
}
