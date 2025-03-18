using EcommerceAPI.Application.Validators;
using EcommerceAPI.DTOs;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace EcommerceAPI.Tests.Validators;
public class CarrinhoCreateDtoValidatorTests
{
    private readonly CarrinhoCreateDtoValidator _validate;

    public CarrinhoCreateDtoValidatorTests()
    {
        _validate = new CarrinhoCreateDtoValidator();
    }

    [Fact]
    public void Deve_Passar_Quando_Dados_Forem_Validos()
    {
        var dto = new CarrinhoCreateDto
        {
            UsuarioId = 1,
            Total = 2000
        };

        var result = _validate.TestValidate(dto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Falhar_Quando_UsuarioId_For_Zero()
    {
        var dto = new CarrinhoCreateDto
        {
            UsuarioId = 0,
            Total = 2000
        };

        var result = _validate.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(carrinho => carrinho.UsuarioId)
            .WithErrorMessage("O ID do usuário deve ser maior que zero");
    }

    [Fact]
    public void Deve_Falhar_Quando_Total_Do_Carrinho_For_Menor_Que_Zero()
    {
        var dto = new CarrinhoCreateDto
        {
            UsuarioId = 1,
            Total = -1
        };

        var result = _validate.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(carrinho => carrinho.Total)
            .WithErrorMessage("O total do carrinho não pode ser negativo");
    }
}
