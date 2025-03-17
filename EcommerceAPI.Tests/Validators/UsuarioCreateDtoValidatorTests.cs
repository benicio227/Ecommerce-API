using EcommerceAPI.Application.Validators;
using EcommerceAPI.DTOs;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace EcommerceAPI.Tests.Validators;
public class UsuarioCreateDtoValidatorTests

{
    private readonly UsuarioCreateDtoValidator _validator;

    public UsuarioCreateDtoValidatorTests()
    {
        _validator = new UsuarioCreateDtoValidator();
    }

    [Fact]
    public void Deve_Passar_Quando_Dados_Forem_Validos()
    {
        var dto = new UsuarioCreateDto
        {
            Name = "João da Silva",
            Email = "joao@email.com",
            Senha = "123456",
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = "1122334456"
        };

        var result = _validator.TestValidate(dto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Deve_Falhar_Quando_Nome_Estiver_Vazio()
    {
        var dto = new UsuarioCreateDto
        {
            Name = "",
            Email = "joao@email.com",
            Senha = "123456",
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = "1222333445"
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(usuario => usuario.Name)
            .WithErrorMessage("O nome é obrigatório");
    }

    [Fact]
    public void Deve_Falhar_Quando_Email_For_Invalido()
    {
        var dto = new UsuarioCreateDto
        {
            Name = "João",
            Email = "Email-invalido",
            Senha = "123456",
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = "1199999999"
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(usuario => usuario.Email)
            .WithErrorMessage("O e-mail informado não é válido");
    }

    [Theory]
    [InlineData("1199999999")]
    [InlineData("11999999999")]
    public void Deve_Passar_Quando_Telefone_For_Valido(string telefoneValido)
    {
        var dto = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@email.com",
            Senha = "123456",
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = telefoneValido
        };

        var result = _validator.TestValidate(dto);

        result.ShouldNotHaveValidationErrorFor(usuario => usuario.Telefone);
    }

    [Theory]
    [InlineData("11999999")]
    [InlineData("119999999999")]
    [InlineData("11-999-999-999")]
    [InlineData("(11)9999999999")]
    [InlineData("abc999999999")]
    public void Deve_Falhar_Qaundo_Telefone_For_Invalido(string telefonInvalido)
    {
        var dto = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@email.com",
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Senha = "123456",
            Telefone = telefonInvalido
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(usuario => usuario.Telefone)
            .WithErrorMessage("O telefone deve ter 10 ou 11 dígitos.");
    }

    [Fact]
    public void Nao_Deve_Dar_Erro_Quando_For_Nulo_Ou_Vazio()
    {
        var dtoComTelefoneNulo = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@hotmail.com",
            Senha = "123456",
            Telefone = null
        };

        var dtoComTelefoneVazio = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@hotmail.com",
            Senha = "123456",
            Telefone = ""
        };

        var result1 = _validator.TestValidate(dtoComTelefoneNulo);
        var result2 = _validator.TestValidate(dtoComTelefoneVazio);

        result1.ShouldNotHaveValidationErrorFor(u => u.Telefone);
        result2.ShouldNotHaveValidationErrorFor(u => u.Telefone);
    }

    [Theory]
    [InlineData("Bb@12345678")]
    [InlineData("Senha@2025")]
 
    public void Deve_Passar_Quando_Senha_For_Valida(string senhaValida)
    {
        var dto = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@email.com",
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = "19999999999",
            Senha = senhaValida
        };

        var result = _validator.TestValidate(dto);

        result.ShouldNotHaveValidationErrorFor(usuario => usuario.Senha);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Deve_Falhar_Quando_Senha_Estiver_Vazia_Ou_Nula(string senhaInvalida)
    {
      
        var dto = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@email.com",
            Senha = senhaInvalida,
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = "11999999999"
        };

        
        var result = _validator.TestValidate(dto);

      
        result.ShouldHaveValidationErrorFor(u => u.Senha)
            .WithErrorMessage("A senha é obrigatória");
    }

    [Theory]
    [InlineData("123")]
    [InlineData("12345")] 
    public void Deve_Falhar_Quando_Senha_Tiver_Menos_De_8_Caracteres(string senhaCurta)
    {
      
        var dto = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@email.com",
            Senha = senhaCurta,
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = "11999999999"
        };

       
        var result = _validator.TestValidate(dto);

        
        result.ShouldHaveValidationErrorFor(usuario => usuario.Senha)
            .WithErrorMessage("A senha deve ter no mínimo 8 caracteres");
    }

    [Fact]
    public void Deve_Falhar_Quando_Senha_Nao_Tiver_Ao_Menos_Uma_Letra_Maiuscula()
    {
        var dto = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@email.com",
            Senha = "20100119@b",
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = "1223344556"
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(usuario => usuario.Senha)
            .WithErrorMessage("A senha deve conter pelo menos uma letra maiúscula");
    }

    [Fact]
    public void Deve_Falhar_Quando_Senha_Nao_Tiver_Ao_Menos_Uma_Letra_Minuscula()
    {
        var dto = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@email.com",
            Senha = "20100119@B",
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = "12345678910"
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(usuario => usuario.Senha)
            .WithErrorMessage("A senha deve conter pelo menos uma letra minúscula");
    }

    [Fact]
    public void Deve_Falhar_Quando_Senha_Nao_Tiver_Ao_Menos_Um_Numero()
    {
        var dto = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@email.com",
            Senha = "MinhaSenha@",
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = "12345678910"
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(usuario => usuario.Senha)
            .WithErrorMessage("A senha deve conter pelo menos um número");
    }

    [Fact]
    public void Deve_Falhar_Quando_Senha_Nao_Tiver_Ao_Menos_Um_Caractere_Especial()
    {
        var dto = new UsuarioCreateDto
        {
            Name = "João",
            Email = "joao@email.com",
            Senha = "MinhaSenha123",
            Perfil = Domain.Enums.PerfilUsuario.Cliente,
            Telefone = "12345678910"
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(usuario => usuario.Senha)
            .WithErrorMessage("A senha deve conter pelo menos um caractere especial");
    }
}
