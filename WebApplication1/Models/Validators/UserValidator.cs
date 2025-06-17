using FluentValidation;

namespace WebApplication1.Models.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() {
            RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Nome Obrigatório");

            RuleFor(p => p.Phone)
            .NotEmpty()
            .WithMessage("Telefone Obrigatório");

            RuleFor(p => p.Cep)
            .NotEmpty()
            .WithMessage("Cep Obrigatório");
        }
    }
}
