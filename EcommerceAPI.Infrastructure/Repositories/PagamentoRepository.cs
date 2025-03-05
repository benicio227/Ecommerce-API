using EcommerceAPI.Data;
using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly EcommerceDbContext _context;
    private readonly IPedidoRepository _pedidoRepository;
    public PagamentoRepository(EcommerceDbContext context, IPedidoRepository pedidRepository)
    {
        _context = context;
        _pedidoRepository = pedidRepository;
    }
    public async Task<IEnumerable<Pagamento>> ObterPagamentos(int id)
    {
        return await _context.Pagamentos.AsNoTracking().ToListAsync();
    }
    public async Task<Pagamento?> ObterPagamentoPorId(int id)
    {
        return await _context.Pagamentos.AsNoTracking().FirstOrDefaultAsync(pagamento => pagamento.Id == id);

    }
    public async Task<Pagamento> AdicionarPagamento(Pagamento pagamento)
    {
        _context.Pagamentos.Add(pagamento);
        await _context.SaveChangesAsync();

        return pagamento;
    }
    public async Task<bool> SalvarAlteracoesPagamento(Pagamento pagamento)
    {
        _context.Update(pagamento);
        return await _context.SaveChangesAsync() > 0;

    }
    public async Task<bool> RemoverPagamentoPorId(int id)
    {
        var pagamento = await _context.Pagamentos.FirstOrDefaultAsync(pagamento => pagamento.Id == id);

        if (pagamento is null)
        {
            return false;
        }

        _context.Pagamentos.Remove(pagamento);
        await _context.SaveChangesAsync();

        return true;
    }

}
