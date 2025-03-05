namespace EcommerceAPI.DTOs;

public class PedidoResponseDto
{
    public int Id {  get; set; }
    public int UsuarioId {  get; set; }
    public DateTime DataPedido { get; set; }
    public string Status { get; set; } = "Pendente";
    public decimal Total {  get; set; }
    public List<PedidoItemResponseDto> PedidoItems { get; set; } = new();

}
