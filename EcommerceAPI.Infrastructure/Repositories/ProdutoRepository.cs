using EcommerceAPI.Data;
using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly EcommerceDbContext _context;
    public ProdutoRepository(EcommerceDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Produto>> ObterProdutos()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }
    public async Task<Produto?> ObterProdutoPorId(int id)
    {
        return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(produto => produto.Id == id);
    }
    public async Task AdicionarProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
    }

    public async Task SalvarAlteracoesProduto(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemoverProdutoPorId(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(produto => produto.Id == id);

        if (produto is null)
        {
            return false;
        }
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Produto>> ObterProdutosPorIds(List<int> produtoIds)
    {
        return await _context.Produtos
            .Where(p => produtoIds.Contains(p.Id))
            .ToListAsync();
    }


}
