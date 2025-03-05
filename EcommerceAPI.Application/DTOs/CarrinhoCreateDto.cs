using System.Text.Json.Serialization;

namespace EcommerceAPI.DTOs;

public class CarrinhoCreateDto
{
    public int UsuarioId { get; set; }
    public decimal Total { get; set; }
}
