using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Services;
public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> ObterUsuarios();
    Task<Usuario?> ObterUsuarioPorId(int id);
    Task<Usuario?> CriarUsuario(Usuario usuario);
    Task<bool> EditarUsuario(Usuario usuario);
    Task<bool> DeletarUsuarioPorId(int id);
    Task<LoginResponseDto> LogarUsuario(RequestLoginDto loginDto);
}
