using EcommerceAPI.Application.Validators;
using EcommerceAPI.DTOs;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace EcommerceAPI.Tests.Validators;
public class CarrinhoItemCreateDtoValidatorTests
{
    private readonly CarrinhoItemCreateDtoValidator _validate;

    public CarrinhoItemCreateDtoValidatorTests()
    {
        _validate = new CarrinhoItemCreateDtoValidator();
    }

    [Fact]
    public void Deve_Passar_Quando_Dados_Forem_Validos()
    {
        var dto = new CarrinhoItemCreateDto
        {
            CarrinhoId = 1,
            ProdutoId = 1,
            Quantidade = 20
        };

        var result = _validate.TestValidate(dto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Falhar_Quando_CarrinhoId_For_Igual_A_Zero()
    {
        var dto = new CarrinhoItemCreateDto
        {
            CarrinhoId = 0,
            ProdutoId = 1,
            Quantidade = 20
        };

        var result = _validate.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(carrinhoItem => carrinhoItem.CarrinhoId)
            .WithErrorMessage("O CarrinhoId deve ser maior que zero");
    }

    [Fact]
    public void Deve_Falhar_Quando_ProdutoId_For_Igual_A_Zero()
    {
        var dto = new CarrinhoItemCreateDto
        {
            CarrinhoId = 1,
            ProdutoId = 0,
            Quantidade = 20
        };

        var result = _validate.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(carrinhoItem => carrinhoItem.ProdutoId)
            .WithErrorMessage("O ProdutoId deve ser maior que zero");
    }

    [Fact]
    public void Deve_Falhar_Quando_Quantidade_For_Menor_Que_Zero()
    {
        var dto = new CarrinhoItemCreateDto
        {
            CarrinhoId = 1,
            ProdutoId = 1,
            Quantidade = -20
        };

        var result = _validate.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(carrinhoItem => carrinhoItem.Quantidade)
            .WithErrorMessage("A quantidade deve ser maior que zero");
    }
}
