using EcommerceAPI.DTOs;
using FluentValidation;

namespace EcommerceAPI.Application.Validators;
public class PedidoItemCreateDtoValidator : AbstractValidator<PedidoItemCreateDto>
{
    public PedidoItemCreateDtoValidator()
    {
        RuleFor(produtoItem => produtoItem.ProdutoId)
            .GreaterThan(0).WithMessage("O Id do produto deve ser maior que zero");

        RuleFor(produtoItem => produtoItem.Quantidade)
            .GreaterThan(0).WithMessage("A quantidade do item deve ser maior que zero");

        RuleFor(produtoItem => produtoItem.PrecoUnitario)
            .GreaterThan(0).WithMessage("O preço unitário do produto deve ser maior que zero");
    }
}
