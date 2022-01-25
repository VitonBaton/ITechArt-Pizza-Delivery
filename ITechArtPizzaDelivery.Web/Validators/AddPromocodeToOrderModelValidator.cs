using FluentValidation;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Validators
{
    public class AddPromocodeToOrderModelValidator : AbstractValidator<AddPromocodeToOrderModel>
    {
        public AddPromocodeToOrderModelValidator()
        {
            RuleFor(p => p.PromocodeName)
                .NotEmpty()
                .NotNull();
        }
    }
}