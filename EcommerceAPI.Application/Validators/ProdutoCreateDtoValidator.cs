using EcommerceAPI.DTOs;
using FluentValidation;


namespace EcommerceAPI.Application.Validators;
public class ProdutoCreateDtoValidator : AbstractValidator<ProdutoCreateDto>
{
    public ProdutoCreateDtoValidator()
    {
        RuleFor(produto => produto.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres");

        RuleFor(produto => produto.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória")
            .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres");

        RuleFor(produto => produto.Preco)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero");

        RuleFor(produto => produto.Estoque)
            .GreaterThanOrEqualTo(0).WithMessage("O estoque não pode ser negativo");

        RuleFor(produto => produto.CategoriaId)
            .GreaterThan(0).WithMessage("A categoria deve ser um identificador válido");
    }
}
