using FluentValidation;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Validators
{
    public class PostPizzaToCartModelValidator : AbstractValidator<PostPizzaToCartModel>
    {
        public PostPizzaToCartModelValidator()
        {
            RuleFor(p => p.PizzaId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(p => p.PizzasCount)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
        }
    }
}