using EcommerceAPI.Domain.Enums;

namespace EcommerceAPI.DTOs;

public class PedidoCreateDto
{
    public int Id {  get; set; }
    public int UsuarioId {  get; set; }
    public DateTime DataPedido {  get; set; } = DateTime.UtcNow;
    public StatusPedido Status { get; set; } = StatusPedido.Pendente;
}
