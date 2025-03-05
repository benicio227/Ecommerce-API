using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Application.Services;
public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public async Task<IEnumerable<Produto>> ObterProdutos()
    {
        return await _produtoRepository.ObterProdutos();
    }
    public async Task<Produto> ObterProdutoPorId(int id)
    {
        var produto = await _produtoRepository.ObterProdutoPorId(id);
        if (produto is null)
        {
            throw new KeyNotFoundException("Produto não encontrado.");
        }

        return produto;
    }
    public async Task CriarProduto(Produto produto)
    {

        if (string.IsNullOrWhiteSpace(produto.Nome))
        {
            throw new ArgumentException("O nome do produto é obrigatório.");
        }

        await _produtoRepository.AdicionarProduto(produto);
    }

    public async Task EditarProduto(Produto produto)
    {
        var produtoExistente = await _produtoRepository.ObterProdutoPorId(produto.Id);
        if (produtoExistente is null)
        {
            throw new KeyNotFoundException("Produto não encontrado.");
        }

        produtoExistente.Nome = produto.Nome;
        produtoExistente.Preco = produto.Preco;
        produtoExistente.Estoque = produto.Estoque;

        await _produtoRepository.SalvarAlteracoesProduto(produtoExistente);
    }
    public async Task ExcluirProdutoPorId(int id)
    {
        var deletado = await _produtoRepository.RemoverProdutoPorId(id);
        if (!deletado)
        {
            throw new KeyNotFoundException("Produto não encontrado.");
        }

    }
}
