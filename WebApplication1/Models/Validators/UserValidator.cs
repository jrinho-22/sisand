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


            RuleFor(p => p.Cidade)
            .NotEmpty()
            .WithMessage("Cidade Obrigatório");

            RuleFor(p => p.Bairro)
            .NotEmpty()
            .WithMessage("Bairro Obrigatório");

            RuleFor(p => p.Cep)
            .NotEmpty()
            .WithMessage("Cep Obrigatório");

            RuleFor(p => p.Logradouro)
            .NotEmpty()
            .WithMessage("Logradouro Obrigatório");

            RuleFor(p => p.Numero)
            .NotEmpty()
            .WithMessage("Numero Obrigatório");
        }
    }
}
