using EcommerceAPI.Data;
using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EcommerceDbContext _context; 
    public UsuarioRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> ObterUsuarios()
    {
        return await _context.Usuarios.AsNoTracking().ToListAsync();
    }

    public async Task<Usuario?> ObterUsuarioPorId(int id)
    {
        return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(usuario => usuario.Id == id);

    }
    public async Task<Usuario?> AdicionarUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }
    public async Task<bool> SalvarAlteracoesUsuario(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoverUsuarioPorId(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario is null)
        {
            return false;
        }
        _context.Usuarios.Remove(usuario);
        return await _context.SaveChangesAsync() > 0;
        
    }

    public async Task<Usuario?> ObterUsuarioPorEmail(string email)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == email);

        return usuario;
    }
}
