using EcommerceAPI.Application.Validators;
using EcommerceAPI.DTOs;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace EcommerceAPI.Tests.Validators;
public class PagamentoCreateDtoValidatorTests
{
    private readonly PagamentoCreateDtoValidator _validate;

    public PagamentoCreateDtoValidatorTests()
    {
        _validate = new PagamentoCreateDtoValidator();
    }

    [Fact]
    public void Deve_Passar_Quando_Dados_Forem_Validos()
    {
        var dto = new PagamentoCreateDto
        {
            PedidoId = 1,
            Metodo = Domain.Enums.MetodoPagamento.CartaoCredito
        };

        var result = _validate.TestValidate(dto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Falhar_Quando_PedidoId_For_Zero()
    {
        var dto = new PagamentoCreateDto
        {
            PedidoId = 0,
            Metodo = Domain.Enums.MetodoPagamento.CartaoCredito
        };

        var result = _validate.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(pagamento => pagamento.PedidoId)
            .WithErrorMessage("O PedidoId dev ser maior que zero");
    }

    [Fact]
    public void Deve_Falhar_Quando_Metodo_Pagamento_Estiver_Fora_Do_Range()
    {
        var dto = new PagamentoCreateDto
        {
            PedidoId = 1,
            Metodo = (Domain.Enums.MetodoPagamento)999
        };

        var result = _validate.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(pagamento => pagamento.Metodo)
            .WithErrorMessage("O método de pagamento informado não é válido");
    }
}
