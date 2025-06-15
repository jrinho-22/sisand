using FluentValidation;

namespace WebApplication1.Models.Validators
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("Email Required");

            RuleFor(p => p.PasswordHash)
                .NotEmpty()
                .WithMessage("Password Required");
        }
    }
}
