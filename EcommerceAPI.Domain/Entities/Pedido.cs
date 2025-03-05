using EcommerceAPI.Domain.Enums;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Entities;

public class Pedido
{
    public int Id {  get; set; }
    public int UsuarioId { get; set; }

    [JsonIgnore]
    public Usuario? Usuario { get; set; }
    public DateTime DataPedido { get; set; } = DateTime.UtcNow;
    public StatusPedido Status { get; set; } = StatusPedido.Pendente;
    public List<PedidoItem> PedidoItems { get; set; } = new();
    
    public decimal Total()
    {
        return PedidoItems.Sum(item => item.Quantidade * item.PrecoUnitario);
    }
}
