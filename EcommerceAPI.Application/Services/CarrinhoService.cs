using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Application.Services;
public class CarrinhoService : ICarrinhoService
{
    private readonly ICarrinhoRepository _carrinhoRepository;
    private readonly IProdutoRepository _produtoRepository;

    public CarrinhoService(ICarrinhoRepository carrinhoRepository, IProdutoRepository produtoRepository)
    {
        _carrinhoRepository = carrinhoRepository;
        _produtoRepository = produtoRepository;
    }
    public async Task<CarrinhoItem?> AdicionarItemAoCarrinho(int carrinhoId, int produtoId, int quantidade)
    {
        // Chamei o método ObterCarrinhoPorId porque a entidade CarrinhoItem pertence a um carrinho. Logo, tenho que 
        // encontrar a entidade Carrinho na qual quero adicionar o CarrinhoItem, já que existem vários carrinhos.
        var carrinho = await _carrinhoRepository.ObterCarrinhoPorId(carrinhoId);

        if (carrinho == null)
        {
            throw new InvalidOperationException("Carrinho não encontrado.");
        } 

        // Chamei o método ObterProdutoPorId porque a entidade CarrinhoItem pertence a um produto. Logo, tenho que
        // encontrar a entidade produto na qual quero adicionar o CarrinhoItem, já que existem vários produtos.
        var produto = await _produtoRepository.ObterProdutoPorId(produtoId);

        if (produto == null)
        {
            throw new InvalidOperationException("Produto não encontrado.");
        } 


        var novoItem = new CarrinhoItem
        {
            ProdutoId = produtoId,
            Quantidade = quantidade,
            PrecoUnitario = produto.Preco // Dessa forma não preciso preencher a propriedade PrecoUnitário. O sistema é que faz isso
        };

        carrinho.CarrinhoItems.Add(novoItem);
        carrinho.AtualizarTotal();

        await _carrinhoRepository.SalvarAlteracoesCarrinho(carrinho);
        return novoItem;
    }

    public async Task<Carrinho?> CriarCarrinho(int usuarioId)
    {
        // O objetivo do método ObterCarrinhoPorUsuarioId é VERIFICAR SE O USUÁRIO JÁ TEM UM CARRINHO ATIVO antes
        // de criar um novo carrinho.
        var carrinho = await _carrinhoRepository.ObterCarrinhoPorUsuarioId(usuarioId);

        if (carrinho != null)
        {
            carrinho.AtualizarTotal();
            return carrinho;
        }

        // Aqui vinculamos o usuarioId(id do ususario) ao novo carrinho criado, porque lembre que a entidade carrinho
        // tem um relacionamento com o usuário(um carrinho pertence a um usuario).Existe uma propriedade
        // UsuarioId na entidade Carrinho
        var novoCarrinho = new Carrinho
        {
            UsuarioId = usuarioId
        };

        // Aqui passamos para o repositorio o novo carrinho já com o Id do usuario ao qual o carrinho pertence
        return await _carrinhoRepository.AdicionarCarrinho(novoCarrinho);
    }

    public async Task<Carrinho?> ObterCarrinhoPorUsuarioId(int usuarioId)
    {
        var carrinho = await _carrinhoRepository.ObterCarrinhoPorUsuarioId(usuarioId);
        if (carrinho != null)
        {
            carrinho.AtualizarTotal();
        }
        return carrinho;
    }

    public async Task<bool> ExcluirCarrinhoPorId(int id)
    {
        return await _carrinhoRepository.RemoverCarrinhoPorId(id);
    }

    public async Task LimparCarrinho(int usuarioId)
    {
        var carrinho = await _carrinhoRepository.ObterCarrinhoPorUsuarioId(usuarioId);
        if (carrinho == null)
        {
            throw new InvalidOperationException("Carrinho não encontrado.");
        } 

        carrinho.CarrinhoItems.Clear();
        carrinho.AtualizarTotal();

        await _carrinhoRepository.SalvarAlteracoesCarrinho(carrinho);
    }
}
