using FluentValidation;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        private readonly string _allowedUserNameCharacters =
            @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789\-\._\@\+";
        public LoginModelValidator()
        {
            RuleFor(l => l.Login)
                .NotEmpty()
                .NotNull()
                .Matches($"[{_allowedUserNameCharacters}]*")
                .MinimumLength(3);
            RuleFor(l => l.Password)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d])")
                .MinimumLength(6);
        }
    }
}