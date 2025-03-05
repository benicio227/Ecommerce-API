using EcommerceAPI.DTOs;
using FluentValidation;

namespace EcommerceAPI.Application.Validators;
public class CarrinhoItemCreateDtoValidator : AbstractValidator<CarrinhoItemCreateDto>
{
    public CarrinhoItemCreateDtoValidator()
    {
        RuleFor(carrinhoItem => carrinhoItem.CarrinhoId)
            .GreaterThan(0).WithMessage("O CarrinhoId deve ser maior que zero");

        RuleFor(carrinhoItem => carrinhoItem.ProdutoId)
            .GreaterThan(0).WithMessage("O ProdutoId deve ser maior que zero");

        RuleFor(carrinhoItem => carrinhoItem.Quantidade)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero");

    }
}
