using EcommerceAPI.DTOs;
using FluentValidation;

namespace EcommerceAPI.Application.Validators;
public class CategoriaCreateDtoValidator : AbstractValidator<CategoriaCreateDto>
{
    public CategoriaCreateDtoValidator()
    {
        RuleFor(categoria => categoria.Nome)
            .NotNull().WithMessage("O nome da categoria é obrigatório")
            .Must(nome => !string.IsNullOrWhiteSpace(nome));
    }
}
