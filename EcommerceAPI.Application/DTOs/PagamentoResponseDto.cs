namespace EcommerceAPI.DTOs;

public class PagamentoResponseDto
{
    public int Id {  get; set; }
    public int PedidoId {  get; set; }
    public DateTime DataPagamento {  get; set; }
    public decimal Valor {  get; set; }
    public int Metodo {  get; set; }
    public int Status {  get; set; }
}
