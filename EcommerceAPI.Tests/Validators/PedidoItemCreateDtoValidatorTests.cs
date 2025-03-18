using EcommerceAPI.Application.Validators;
using EcommerceAPI.DTOs;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace EcommerceAPI.Tests.Validators;
public class PedidoItemCreateDtoValidatorTests
{
    private readonly PedidoItemCreateDtoValidator _validate;

    public PedidoItemCreateDtoValidatorTests()
    {
        _validate = new PedidoItemCreateDtoValidator();
    }

    [Fact]
    public void Deve_Passar_Quando_Dados_Forem_Validos()
    {
        var dto = new PedidoItemCreateDto
        {
            ProdutoId = 1,
            Quantidade = 20,
            PrecoUnitario = 200
        };

        var result = _validate.TestValidate(dto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Falhar_Quando_Id_Produto_For_Igual_A_Zero()
    {
        var dto = new PedidoItemCreateDto
        {
            ProdutoId = 0,
            Quantidade = 20,
            PrecoUnitario = 200
        };

        var result = _validate.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(pedidoItem => pedidoItem.ProdutoId)
            .WithErrorMessage("O Id do produto deve ser maior que zero");
    }

    [Fact]
    public void Deve_Falhar_Quando_A_Quantidade_For_Igual_A_Zero()
    {
        var dto = new PedidoItemCreateDto
        {
            ProdutoId = 1,
            Quantidade = 0,
            PrecoUnitario = 200
        };

        var result = _validate.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(pedidoItem => pedidoItem.Quantidade)
            .WithErrorMessage("A quantidade do item deve ser maior que zero");
    }

    [Fact]
    public void Deve_Falhar_Qaundo_O_Preco_Unitario_For_Zero()
    {
        var dto = new PedidoItemCreateDto
        {
            ProdutoId = 1,
            Quantidade = 20,
            PrecoUnitario = 0
        };

        var result = _validate.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(pedidoItem => pedidoItem.PrecoUnitario)
            .WithErrorMessage("O preço unitário do produto deve ser maior que zero");
    }
}
