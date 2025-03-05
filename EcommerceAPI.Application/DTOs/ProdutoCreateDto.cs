namespace EcommerceAPI.DTOs;

public class ProdutoCreateDto
{
    public int Id {  get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao {  get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Estoque {  get; set; }
    public int CategoriaId { get; set; }
}
