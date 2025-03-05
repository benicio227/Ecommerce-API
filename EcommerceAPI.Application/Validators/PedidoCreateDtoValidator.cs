using EcommerceAPI.Domain.Enums;
using EcommerceAPI.DTOs;
using FluentValidation;

namespace EcommerceAPI.Application.Validators;
public class PedidoCreateDtoValidator : AbstractValidator<PedidoCreateDto>
{
    public PedidoCreateDtoValidator()
    {
        RuleFor(pedido => pedido.UsuarioId)
            .GreaterThan(0).WithMessage("O usuário é obrigatório e deve ser válido");

        RuleFor(pedido => pedido.DataPedido)
            .NotEmpty().WithMessage("A data do pedido é obrigatória")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data do pedido não pode estar no futuro");

        RuleFor(pedido => pedido.Status)
            .IsInEnum()
            .WithMessage("O status informado não é válido");
    }
}
