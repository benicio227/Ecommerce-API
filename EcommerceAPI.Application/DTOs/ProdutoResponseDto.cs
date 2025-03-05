namespace EcommerceAPI.DTOs;

public class ProdutoResponseDto
{
    public int Id {  get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao {  get; set; } = string.Empty;
    public string Preco {  get; set; } = string.Empty;
    public int Estoque {  get; set; }
    public int CategoriaId {  get; set; }
}
