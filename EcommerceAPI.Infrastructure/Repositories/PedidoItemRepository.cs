using EcommerceAPI.Data;
using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories;

public class PedidoItemRepository : IPedidoItemRepository
{
    private readonly EcommerceDbContext _context;
    public PedidoItemRepository(EcommerceDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<PedidoItem>> ObterPedidosItem()
    {
        return await _context.PedidoItens
            .AsNoTracking()
            .Include(pedidoItem => pedidoItem.Produto)
            .ToListAsync();
    }
    public async Task<PedidoItem?> ObterPedidoItemPorId(int id)
    {
        return await _context.PedidoItens
            .AsNoTracking()
            .Include(pedidoItem => pedidoItem.Produto)
            .FirstOrDefaultAsync(pedidoItem => pedidoItem.Id == id);
    }
    public async Task<PedidoItem?> AdicionarPedidoItem(PedidoItem pedidoItem)
    {
        _context.PedidoItens.Add(pedidoItem);
        await _context.SaveChangesAsync();
        return pedidoItem;
    }

    public async Task<bool> RemoverPedidoItemPorId(int id)
    {
        var pedidoItem = await _context.PedidoItens.FindAsync(id);
        if (pedidoItem is null)
        {
            return false;
        }
        _context.PedidoItens.Remove(pedidoItem);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SalvarAlteracoesPedidoItem(PedidoItem pedidoItem)
    {
        _context.Update(pedidoItem);
        await _context.SaveChangesAsync();
        return true;
    }
}
