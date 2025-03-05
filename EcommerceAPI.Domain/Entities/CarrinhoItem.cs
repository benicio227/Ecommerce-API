using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Entities;

public class CarrinhoItem
{
    public int Id { get; set; }
    public int CarrinhoId { get; set; }

    [JsonIgnore]
    public Carrinho? Carrinho { get; set; }
    public int ProdutoId { get; set; }

    [JsonIgnore]
    public Produto? Produto { get; set; }
    public int Quantidade {  get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoUnitario {  get; set; }
}
 
