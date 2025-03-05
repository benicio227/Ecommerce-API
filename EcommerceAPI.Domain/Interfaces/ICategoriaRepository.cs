using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Repositories;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> ObterCategorias();
    Task<Categoria?> ObterCategoriaPorId(int id);
    Task<Categoria?> ObterCategoriaPorNome(string nome);
    Task<Categoria> AdicionarCategoria(Categoria categoria);
    Task<bool> SalvarAlteracoesCategoria(Categoria categoria);
    Task<bool> RemoverCategoriaPorId(int id);
}
