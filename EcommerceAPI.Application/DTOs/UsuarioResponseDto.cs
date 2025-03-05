namespace EcommerceAPI.DTOs;

public class UsuarioResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Perfil { get; set; } = "Cliente";
    public string? Telefone {  get; set; }
}
