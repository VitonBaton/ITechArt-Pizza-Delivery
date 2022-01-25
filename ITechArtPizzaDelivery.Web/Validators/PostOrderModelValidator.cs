using FluentValidation;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Validators
{
    public class PostOrderModelValidator : AbstractValidator<PostOrderModel>
    {
        public PostOrderModelValidator()
        {
            RuleFor(o => o.Address)
                .NotEmpty()
                .NotNull();
            RuleFor(o => o.DeliveryId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(o => o.PaymentId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
        }
    }
}