using FluentValidation;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Validators
{
    public class RegistrationModelValidator : AbstractValidator<RegistrationModel>
    {
        private readonly string _allowedUserNameCharacters =
            @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789\-\._\@\+";
        
        public RegistrationModelValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();
            RuleFor(r => r.UserName)
                .NotEmpty()
                .NotNull()
                .Matches($"[{_allowedUserNameCharacters}]*")
                .MinimumLength(6);
            RuleFor(r => r.Password)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d])")
                .MinimumLength(6);
        }
    }
}