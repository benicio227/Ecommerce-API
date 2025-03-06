using EcommerceAPI.Domain.Enums;

namespace EcommerceAPI.Domain.Entities;

public class Usuario
{
    public int Id {  get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public PerfilUsuario Perfil {  get; set; } = PerfilUsuario.Cliente;
    public string? Telefone {  get; set; }

}
