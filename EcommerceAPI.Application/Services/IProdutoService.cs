using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Services;
public interface IProdutoService
{
    Task<IEnumerable<Produto>> ObterProdutos();
    Task<Produto> ObterProdutoPorId(int id);
    Task CriarProduto(Produto produto);
    Task EditarProduto(Produto produto);
    Task ExcluirProdutoPorId(int id);
}
