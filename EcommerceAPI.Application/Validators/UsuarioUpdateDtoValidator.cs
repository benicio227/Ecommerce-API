using EcommerceAPI.DTOs;
using FluentValidation;

namespace EcommerceAPI.Application.Validators;
public class UsuarioUpdateDtoValidator : AbstractValidator<UsuarioUpdateDto>
{
    public UsuarioUpdateDtoValidator()
    {
        RuleFor(u => u.Id)
            .GreaterThan(0).WithMessage("O ID deve ser maior que zero.");

        RuleFor(u => u.Nome)
               .NotEmpty().WithMessage("O nome é obrigatório.")
               .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado não é válido.");

        RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");

        RuleFor(u => u.Perfil)
               .IsInEnum().WithMessage("O perfil deve ser um valor válido");

        RuleFor(u => u.Telefone)
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve ter 10 ou 11 dígitos.")
            .When(u => !string.IsNullOrEmpty(u.Telefone));
    }
}
