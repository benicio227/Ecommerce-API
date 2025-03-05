using EcommerceAPI.DTOs;
using FluentValidation;

namespace EcommerceAPI.Application.Validators;
public class PagamentoCreateDtoValidator : AbstractValidator<PagamentoCreateDto>
{
    public PagamentoCreateDtoValidator()
    {
        RuleFor(pagamento => pagamento.PedidoId)
            .GreaterThan(0).WithMessage("O PedidoId dev ser maior que zero");


        RuleFor(pagamento => pagamento.Metodo)
            .IsInEnum().WithMessage("O método de pagamento informado não é válido");
    }
}
