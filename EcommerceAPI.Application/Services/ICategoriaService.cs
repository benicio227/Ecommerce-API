using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Services;
public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> ObterCategorias();
    Task<Categoria?> ObterCategoriaPorId(int id);
    Task<Categoria> CriarCategoria(Categoria categoria);
    Task<bool> ExcluirCategoriaPorId(int id);
    Task<bool> EditarCategoria(Categoria categoria);
}
