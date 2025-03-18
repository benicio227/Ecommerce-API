using EcommerceAPI.DTOs;
using FluentValidation.TestHelper;
using FluentAssertions;
using EcommerceAPI.Application.Validators;

namespace EcommerceAPI.Tests.Validators;
public class ProdutoCreateDtoValidatorTests
{
    private readonly ProdutoCreateDtoValidator _validator;

    public ProdutoCreateDtoValidatorTests()
    {
        _validator = new ProdutoCreateDtoValidator();
    }

    [Fact]
    public void Deve_Passar_Quando_Dados_Forem_Validos()
    {
        var dto = new ProdutoCreateDto
        {
            Nome = "Geladeira",
            Descricao = "Em promoção",
            Preco = 1200,
            Estoque = 40,
            CategoriaId = 1,
        };

        var result = _validator.TestValidate(dto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Falhar_Quando_Nome_Estiver_Vazio()
    {
        var dto = new ProdutoCreateDto
        {
            Nome = string.Empty,
            Descricao = "Em promoção",
            Preco = 1200,
            Estoque = 40,
            CategoriaId = 1,
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(produto => produto.Nome)
            .WithErrorMessage("O nome é obrigatório");
    }

    [Fact]
    public void Deve_Falhar_Quando_Nome_Tiver_Mais_De_100_Caracteres()
    {
        var dto = new ProdutoCreateDto
        {
            Nome = new string('A', 101),
            Descricao = "Em promoção",
            Preco = 1200,
            Estoque = 40,
            CategoriaId = 1,
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(p => p.Nome)
              .WithErrorMessage("O nome deve ter no máximo 100 caracteres");
    }

    [Fact]
    public void Deve_Falhar_Quando_Descricao_Estiver_Vazia()
    {
        var dto = new ProdutoCreateDto
        {
            Nome = "Geladeira",
            Descricao = string.Empty,
            Preco = 1200,
            Estoque = 40,
            CategoriaId = 1,
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(p => p.Descricao)
              .WithErrorMessage("A descrição é obrigatória");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Deve_Falhar_Quando_Preco_For_Menor_Ou_Igual_A_Zero(decimal preco)
    {
        var dto = new ProdutoCreateDto
        {
            Nome = "Geladeira",
            Descricao = "Em promoção",
            Preco = preco,
            Estoque = 40,
            CategoriaId = 1,
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(p => p.Preco)
              .WithErrorMessage("O preço deve ser maior que zero");
    }


    [Fact]
    public void Deve_Falhar_Quando_Estoque_For_Negativo()
    {
        var dto = new ProdutoCreateDto
        {
            Nome = "Geladeira",
            Descricao = "Em promoção",
            Preco = 1200,
            Estoque = -5,
            CategoriaId = 1,
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(p => p.Estoque)
              .WithErrorMessage("O estoque não pode ser negativo");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Deve_Falhar_Quando_CategoriaId_For_Invalido(int categoriaId)
    {
        var dto = new ProdutoCreateDto
        {
            Nome = "Geladeira",
            Descricao = "Em promoção",
            Preco = 1200,
            Estoque = 40,
            CategoriaId = categoriaId,
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(p => p.CategoriaId)
              .WithErrorMessage("A categoria deve ser um identificador válido");
    }

}
