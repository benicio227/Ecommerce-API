using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Entities;

public class PedidoItem
{
    public int Id { get; set; }
    public int PedidoId {  get; set; }

    [JsonIgnore]
    public Pedido Pedido { get; set; } = null!;
    public int ProdutoId {  get; set; }

    [JsonIgnore]
    public Produto Produto { get; set; } = null!;
    public int Quantidade {  get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoUnitario {  get; set; }
}
