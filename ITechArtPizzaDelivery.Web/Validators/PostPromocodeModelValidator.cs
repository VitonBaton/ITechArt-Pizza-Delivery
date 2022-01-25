using FluentValidation;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Validators
{
    public class PostPromocodeModelValidator : AbstractValidator<PostPromocodeModel>
    {
        public PostPromocodeModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull();
            RuleFor(p => p.Discount)
                .NotNull()
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}