using FluentValidation;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Validators
{
    public class PostPizzaModelValidator : AbstractValidator<PostPizzaModel>
    {
        public PostPizzaModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Price)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}