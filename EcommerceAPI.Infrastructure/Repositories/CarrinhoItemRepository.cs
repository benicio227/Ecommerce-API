using EcommerceAPI.Data;
using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Repositories;

public class CarrinhoItemRepository : ICarrinhoItemRepository
{
    private readonly EcommerceDbContext _context;

    public CarrinhoItemRepository(EcommerceDbContext context)
    {
        _context = context;
    }
    public async Task<CarrinhoItem?> ObterCarrinhoItemPorId(int id)
    {
        return  await _context.CarrinhoItens.FindAsync(id);
    }

    public async Task<bool> RemoverCarrinhoItemPorId(int id)
    {
        var carrinhoItem = _context.CarrinhoItens.Find(id);
        if (carrinhoItem is null)
        {
            return false;
        }

        _context.CarrinhoItens.Remove(carrinhoItem);
        await _context.SaveChangesAsync();
        return true;
    }

}
