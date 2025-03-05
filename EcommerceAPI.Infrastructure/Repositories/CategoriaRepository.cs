using EcommerceAPI.Data;
using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly EcommerceDbContext _context;

    public CategoriaRepository(EcommerceDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Categoria>> ObterCategorias()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }
    public async Task<Categoria?> ObterCategoriaPorId(int id)
    {
        return await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(categoria => categoria.Id == id);
    }
    public async Task<Categoria> AdicionarCategoria(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }
    public async Task<bool> SalvarAlteracoesCategoria(Categoria categoria)
    {
        _context.Update(categoria);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoverCategoriaPorId(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria is null)
        {
            return false;
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Categoria?> ObterCategoriaPorNome(string nome)
    {
        return await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Nome == nome);
    }
}
