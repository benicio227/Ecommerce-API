using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Repositories;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> ObterProdutos();
    Task<Produto?> ObterProdutoPorId(int id);
    Task AdicionarProduto(Produto produto);
    Task SalvarAlteracoesProduto(Produto produto);
    Task<bool> RemoverProdutoPorId(int id);
    Task<List<Produto>> ObterProdutosPorIds(List<int> produtoIds);
}
