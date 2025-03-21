using BCrypt.Net;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Application.Services;
public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<Usuario>> ObterUsuarios()
    {
        return await _usuarioRepository.ObterUsuarios();
    }

    public async Task<Usuario?> ObterUsuarioPorId(int id)
    {
        var usuario = await _usuarioRepository.ObterUsuarioPorId(id);

        if (usuario is null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        return usuario;
    }

    public async Task<Usuario?> CriarUsuario(Usuario usuario)
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash);

        usuario.SenhaHash = senhaHash;

        return await _usuarioRepository.AdicionarUsuario(usuario);
    }

    public async Task<bool> EditarUsuario(Usuario usuario)
    {
        var usuarioExistente = await _usuarioRepository.ObterUsuarioPorId(usuario.Id);
        if (usuarioExistente == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        usuarioExistente.Name = usuario.Name;
        usuarioExistente.Email = usuario.Email;
        usuarioExistente.SenhaHash = usuario.SenhaHash;
        usuarioExistente.Perfil = usuario.Perfil;

        return await _usuarioRepository.SalvarAlteracoesUsuario(usuarioExistente);
    }
    public async Task<bool> DeletarUsuarioPorId(int id)
    {
        var usuarioExistente = await _usuarioRepository.ObterUsuarioPorId(id);
        if (usuarioExistente == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        return await _usuarioRepository.RemoverUsuarioPorId(id);
    }

}
