using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Entities;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao {  get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Preco {  get; set; }
    public int Estoque {  get; set; }
    public int CategoriaId {  get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}
