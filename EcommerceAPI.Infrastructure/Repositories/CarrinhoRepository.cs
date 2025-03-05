using EcommerceAPI.Data;
using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories;

public class CarrinhoRepository : ICarrinhoRepository
{
    private readonly EcommerceDbContext _context;
    public CarrinhoRepository(EcommerceDbContext context)
    {
        _context = context;
    }
    public async Task<Carrinho?> ObterCarrinhoPorUsuarioId(int usuarioId)
    {
        return await _context.Carrinhos
            .Include(carrinho => carrinho.CarrinhoItems)
            .ThenInclude(carrinhoItem => carrinhoItem.Produto)
            .FirstOrDefaultAsync(carrinho => carrinho.UsuarioId == usuarioId);

    }
    public async Task<Carrinho?> AdicionarCarrinho(Carrinho carrinho)
    {
        _context.Carrinhos.Add(carrinho);
        await _context.SaveChangesAsync();
        return carrinho;
    }

    public async Task<bool> SalvarAlteracoesCarrinho(Carrinho carrinho)
    {
        _context.Carrinhos.Update(carrinho);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoverCarrinhoPorId(int id)
    {
        var carrinho = await _context.Carrinhos.FindAsync(id);
        if (carrinho is null)
        {
            return false;
        }
            _context.Carrinhos.Remove(carrinho);
            await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Carrinho?> ObterCarrinhoPorId(int carrinhoId)
    {
        return await _context.Carrinhos
            .AsNoTracking()
            .Include(c => c.CarrinhoItems)
            .ThenInclude(ci => ci.Produto)
            .FirstOrDefaultAsync(c => c.Id == carrinhoId);
    }

}
