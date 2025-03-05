namespace EcommerceAPI.DTOs;

public class PedidoItemCreateDto
{
    public int PedidoId {  get; set; }
    public int ProdutoId {  get; set; }
    public int Quantidade {  get; set; }
    public decimal PrecoUnitario {  get; set; }
}
