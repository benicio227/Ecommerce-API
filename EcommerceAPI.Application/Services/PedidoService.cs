using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Enums;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Application.Services;
public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly ICarrinhoService _carrinhoService;
    public PedidoService(
        IPedidoRepository pedidoRepository,
        IProdutoRepository produtoRepository,
        ICarrinhoService carrinhoService)
    {
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
        _carrinhoService = carrinhoService;
    }
    public async Task<IEnumerable<Pedido>> ObterPedidos()
    {
        return await _pedidoRepository.ObterPedidos();
    }
    public async Task<Pedido?> ObterPedidoPorId(int id)
    {
        return await _pedidoRepository.ObterPedidoPorId(id);
    }

    // Esse método é responsável por transformar o carrinho de compras de um usuário em um pedido confirmado,
    // garantindo que os produtos tenham estoque suficiente
    public async Task<Pedido?> CriarPedido(int usuarioId)
    {
        // Busca o carrinho do usuário pelo ID. Até porque se o usuário não tiver carrinho ou ele estiver vazio,
        // não tem como criar um pedido.
        // Lembre que eu posso ter um usuário e não ter um carrinho associado a ele, por isso precisamos ver
        // se existe um carrinho associado ao usuário
        var carrinho = await _carrinhoService.ObterCarrinhoPorUsuarioId(usuarioId);

        // Se o carrinho não existir(null) ou não tiver itens(CarrinhoItems.Any() retorna false), a função
        // simplesmente retorna null(pedido não pode ser criado)
        if (carrinho is null || !carrinho.CarrinhoItems.Any())
        {
            return null;
        }

        // Cria um novo objeto Pedido com o ID do usuário e a data atual
        // Define o status do pedido como "Pendente"(ainda precisa ser pago e processado)
        // Converte os itens do carrinho(CarrinhoItem) em itens do pedido(PedidoItem)
        var pedido = new Pedido
        {
            UsuarioId = usuarioId,
            DataPedido = DateTime.UtcNow,
            Status = StatusPedido.Pendente,
            PedidoItems = carrinho.CarrinhoItems.Select(carrinhoItem => new PedidoItem
            {
                ProdutoId = carrinhoItem.ProdutoId,
                Quantidade = carrinhoItem.Quantidade,
                PrecoUnitario = carrinhoItem.PrecoUnitario
            }).ToList()
        };

        // Obtem os IDS dos produtos do pedido
        // Usa o repositório para buscar os produtos no banco de dados
        var produtoIds = pedido.PedidoItems.Select(pedidoItem => pedidoItem.ProdutoId).ToList();
        var produtos = await _produtoRepository.ObterProdutosPorIds(produtoIds);
        

        // Percorre cada item do pedido e busca o produto correspondente
        // Se o porduto não existir, lança um erro
        // Se o estoque for menor do que a quantidade pedida, lança um erro
        foreach (var item in pedido.PedidoItems)
        {
            var produto = produtos.FirstOrDefault(produto => produto.Id == item.ProdutoId);

            if (produto == null)
            {
                throw new InvalidOperationException($"Produto {item.ProdutoId} não encontrado.");
            }

            if (produto.Estoque < item.Quantidade)
            {
                throw new InvalidOperationException($"Estoque insuficiente para o produto: {produto.Nome}");
            }

            produto.Estoque -= item.Quantidade;

            // Salva as alterações no banco de dados
            await _produtoRepository.SalvarAlteracoesProduto(produto);
        }

        // O pedido é armazenado no banco, pois a compra foi finalizada
        await _pedidoRepository.AdicionarPedido(pedido);

        // Remove todos os itens do carrinho, pois o pedido já foi criado
        await _carrinhoService.LimparCarrinho(usuarioId);

        // Retorna o pedido pronto para ser processado
        return pedido;
    }
    public async Task<bool> EditarPedido(Pedido pedido)
    {
        var pedidoExistente = await _pedidoRepository.ObterPedidoPorId(pedido.Id);
        if (pedidoExistente == null)
        {
            return false;
        }

        pedidoExistente.Status = pedido.Status;
        pedidoExistente.DataPedido = pedido.DataPedido;
        pedidoExistente.Total();

        await _pedidoRepository.SalvarAlteracoesPedido(pedidoExistente);
        return true;
    }

    public async Task<bool> ExcluirPedidoPorId(int id)
    {
        var pedido = await _pedidoRepository.ObterPedidoPorId(id);
        if (pedido == null)
        {
            return false;
        }

        await _pedidoRepository.RemoverPedidoPorId(pedido.Id);
        return true;
    }

    public async Task<bool> AtualizarStatusPedido(int pedidoId, StatusPedido novoStatus)
    {
        var pedido = await _pedidoRepository.ObterPedidoPorId(pedidoId);
        if (pedido == null)
        {
            return false;
        }

        pedido.Status = novoStatus;
        await _pedidoRepository.SalvarAlteracoesPedido(pedido);
        return true;
    }
}
