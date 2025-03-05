using EcommerceAPI.Data;
using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly EcommerceDbContext _context;
    public PedidoRepository(EcommerceDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Pedido>> ObterPedidos()
    {
        return await _context.Pedidos.AsNoTracking().ToListAsync();
  
    }
    public async Task<Pedido?> ObterPedidoPorId(int id)
    {
        return await BuscarPedidoPorId(id);
    }
    public async Task AdicionarPedido(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> SalvarAlteracoesPedido(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoverPedidoPorId(int id)
    {
        var pedido = await BuscarPedidoPorId(id);

        if (pedido is null)
        {
            return false;
        }
        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<Pedido?> BuscarPedidoPorId(int id)
    {
        return await _context.Pedidos
            .Include(p => p.PedidoItems)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
