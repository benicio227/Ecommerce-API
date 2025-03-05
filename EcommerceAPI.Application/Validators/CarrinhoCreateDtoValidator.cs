using EcommerceAPI.DTOs;
using FluentValidation;

namespace EcommerceAPI.Application.Validators;
public class CarrinhoCreateDtoValidator : AbstractValidator<CarrinhoCreateDto>
{
    public CarrinhoCreateDtoValidator()
    {
        RuleFor(carrinho => carrinho.UsuarioId)
            .GreaterThan(0).WithMessage("O ID do usuário deve ser maior que zero");

        RuleFor(carrinho => carrinho.Total)
            .GreaterThanOrEqualTo(0).WithMessage("O total do carrinho não pode ser negativo");
    }
}
