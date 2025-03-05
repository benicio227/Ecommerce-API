namespace EcommerceAPI.DTOs;

public class CarrinhoItemCreateDto
{
    public int CarrinhoId { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}
