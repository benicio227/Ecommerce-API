using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Entities;

public class Carrinho
{
    public int Id { get; set; }
    public int UsuarioId {  get; set; }

    [JsonIgnore]
    public Usuario? Usuario { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Total {  get; set; }

    public List<CarrinhoItem> CarrinhoItems { get; set; } = new();

    public void AtualizarTotal()
    {
        Total = CarrinhoItems.Sum(item => item.Quantidade * item.PrecoUnitario);
    }
}
