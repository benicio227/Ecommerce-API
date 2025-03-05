using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Repositories;

public interface IPagamentoRepository
{
    Task<IEnumerable<Pagamento>> ObterPagamentos(int id);
    Task<Pagamento?> ObterPagamentoPorId(int id);
    Task<Pagamento> AdicionarPagamento(Pagamento pagamento);
    Task<bool> SalvarAlteracoesPagamento(Pagamento pagamento);
    Task<bool> RemoverPagamentoPorId(int id);
}
