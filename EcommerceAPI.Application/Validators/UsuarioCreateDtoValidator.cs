using EcommerceAPI.DTOs;
using FluentValidation;

namespace EcommerceAPI.Application.Validators;
public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
{
    public UsuarioCreateDtoValidator()
    {
        RuleFor(usuario => usuario.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caractéres");

        RuleFor(usuario => usuario.Email)
            .NotEmpty().WithMessage("O email é obrigatório")
            .EmailAddress().WithMessage("O e-mail informado não é válido");

        RuleFor(usuario => usuario.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres");

        RuleFor(usuario => usuario.Perfil)
            .IsInEnum().WithMessage($"O perfil deve ser um valor válido");

        RuleFor(usuario => usuario.Telefone)
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve ter 10 ou 11 dígitos.")
            .When(usuario => !string.IsNullOrEmpty(usuario.Telefone));

    }
}
