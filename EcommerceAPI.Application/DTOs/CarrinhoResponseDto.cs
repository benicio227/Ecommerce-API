namespace EcommerceAPI.DTOs;

public class CarrinhoResponseDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public decimal Total { get; set; }
    public List<CarrinhoItemResponseDto> CarrinhoItems { get; set; } = new();
}
