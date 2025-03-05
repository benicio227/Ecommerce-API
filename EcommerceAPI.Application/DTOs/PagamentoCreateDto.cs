using EcommerceAPI.Domain.Enums;

namespace EcommerceAPI.DTOs;

public class PagamentoCreateDto
{
    public int Id { get; set; }
    public int PedidoId {  get; set; }
    public decimal Valor {  get; set; }
    public MetodoPagamento Metodo {  get; set; }
}
