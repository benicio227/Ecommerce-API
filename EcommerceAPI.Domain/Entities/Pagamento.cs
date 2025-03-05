using EcommerceAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Entities;

public class Pagamento
{
    public int Id { get; set; }
    public int PedidoId {  get; set; }

    [JsonIgnore]
    public Pedido? Pedido {  get; set; }
    public DateTime DataPagamento { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor {  get; set; }
    public MetodoPagamento Metodo { get; set; }
    public StatusPagamento Status { get; set; } = StatusPagamento.Pendente;
}
