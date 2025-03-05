using EcommerceAPI.Domain.Entities;
using EcommerceAPI.DTOs;

namespace EcommerceAPI.Application.Services;
public interface IPagamentoService
{
    Task<IEnumerable<Pagamento>> ObterPagamentos(int id);
    Task<Pagamento?> ObterPagamentoPorId(int id);
    Task<Pagamento> CriarPagamento(Pagamento pagamento);
    Task<bool> EditarPagamento(PagamentoCreateDto pagamento);
    Task<bool> ExcluirPagamentoPorId(int id);
    Task<bool> CancelarPagamento(int id);
}
