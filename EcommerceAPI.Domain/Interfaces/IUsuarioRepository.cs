using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Repositories;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> ObterUsuarios();
    Task<Usuario?> ObterUsuarioPorId(int id);
    Task<Usuario?> AdicionarUsuario(Usuario usuario);
    Task<bool> SalvarAlteracoesUsuario(Usuario usuario);
    Task<bool> RemoverUsuarioPorId(int id);
}
