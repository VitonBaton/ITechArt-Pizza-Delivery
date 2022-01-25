using FluentValidation;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Validators
{
    public class PostPizzaIngredientsModelValidator : AbstractValidator<PostPizzaIngredientsModel>
    {
        public PostPizzaIngredientsModelValidator()
        {
            RuleFor(p => p.IngredientsId).NotEmpty();
            RuleForEach(p => p.IngredientsId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}