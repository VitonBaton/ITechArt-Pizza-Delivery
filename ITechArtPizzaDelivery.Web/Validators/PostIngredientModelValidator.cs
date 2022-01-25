using FluentValidation;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Validators
{
    public class PostIngredientModelValidator : AbstractValidator<PostIngredientModel>
    {
        public PostIngredientModelValidator()
        {
            RuleFor(i => i.Name).NotEmpty();
        }
    }
}