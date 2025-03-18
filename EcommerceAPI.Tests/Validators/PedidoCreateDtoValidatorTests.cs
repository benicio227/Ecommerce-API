using EcommerceAPI.Application.Validators;
using EcommerceAPI.DTOs;
using FluentAssertions;
using FluentValidation.TestHelper;
using System.Xml.XPath;

namespace EcommerceAPI.Tests.Validators;
public class PedidoCreateDtoValidatorTests
{
    private readonly PedidoCreateDtoValidator _validator;

    public PedidoCreateDtoValidatorTests()
    {
        _validator = new PedidoCreateDtoValidator();
    }

    [Fact]
    public void Deve_Passar_Quando_Dados_Forem_Validos()
    {
        var dto = new PedidoCreateDto
        {
            UsuarioId = 1,
            DataPedido = DateTime.Now,
            Status = Domain.Enums.StatusPedido.Pendente
        };

        var result = _validator.TestValidate(dto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Falhar_Quando_UsuarioId_For_Zero()
    {
        var dto = new PedidoCreateDto
        {
            UsuarioId = 0,
            DataPedido = DateTime.Now,
            Status = Domain.Enums.StatusPedido.Pendente
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(pedido => pedido.UsuarioId)
            .WithErrorMessage("O usuário é obrigatório e deve ser válido");
    }

    [Fact]
    public void Deve_Falhar_Quando_Data_For_Vazia()
    {
        var dto = new PedidoCreateDto
        {
            UsuarioId = 1,
            DataPedido = DateTime.MinValue,
            Status = Domain.Enums.StatusPedido.Pendente
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(pedido => pedido.DataPedido)
            .WithErrorMessage("A data do pedido é obrigatória");
    }

    [Fact]
    public void Deve_Falhar_Quando_Data_For_No_Futuro()
    {
        var dto = new PedidoCreateDto
        {
            UsuarioId = 1,
            DataPedido = DateTime.Now.AddDays(1),
            Status = Domain.Enums.StatusPedido.Pendente
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(pedido => pedido.DataPedido)
            .WithErrorMessage("A data do pedido não pode estar no futuro");
    }

    [Fact]
    public void Deve_Falhar_Quando_Status_Estiver_Fora_Do_Range()
    {
        var dto = new PedidoCreateDto
        {
            UsuarioId = 1,
            DataPedido = DateTime.Now,
            Status = (Domain.Enums.StatusPedido)999
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(pedido => pedido.Status)
            .WithErrorMessage("O status informado não é válido");
    }
}
