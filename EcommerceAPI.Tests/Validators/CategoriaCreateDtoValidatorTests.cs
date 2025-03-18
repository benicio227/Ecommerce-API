using EcommerceAPI.Application.Validators;
using EcommerceAPI.DTOs;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace EcommerceAPI.Tests.Validators;
public class CategoriaCreateDtoValidatorTests
{
    private readonly CategoriaCreateDtoValidator _validator;

    public CategoriaCreateDtoValidatorTests()
    {
        _validator = new CategoriaCreateDtoValidator();    
    }

    [Fact]
    public void Deve_Passar_Quando_Dados_Forem_Validos()
    {
        var dto = new CategoriaCreateDto
        {
            Nome = "Perfumaria"
        };

        var result = _validator.TestValidate(dto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Falhar_Quando_Nome_For_Nulo()
    {
        var dto = new CategoriaCreateDto
        {
            Nome = null!,
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(categoria => categoria.Nome)
            .WithErrorMessage("O nome da categoria é obrigatório");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Deve_Falhar_Quando_Nome_For_Vazio_Ou_Espacos(string nomeInvalido)
    {
        var dto = new CategoriaCreateDto
        {
            Nome = nomeInvalido
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(categoria => categoria.Nome);
    }
}
