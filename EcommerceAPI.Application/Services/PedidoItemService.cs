using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Application.Services;
public class PedidoItemService : IPedidoItemService
{
    private readonly IPedidoItemRepository _pedidoItemRepository;

    public PedidoItemService(IPedidoItemRepository pedidoItemRepository)
    {
        _pedidoItemRepository = pedidoItemRepository;
    }
    public async Task<IEnumerable<PedidoItem>> ObterPedidosItem()
    {
        return await _pedidoItemRepository.ObterPedidosItem();
    }
    public async Task<PedidoItem?> ObterPedidoItemPorId(int id)
    {
        return await _pedidoItemRepository.ObterPedidoItemPorId(id);
    }
    public async Task<PedidoItem?> CriarPedidoItem(PedidoItem pedidoItem)
    {
        return await _pedidoItemRepository.AdicionarPedidoItem(pedidoItem);
    }
    public async Task<bool> ExcluirPedidoItemPorId(int id)
    {
        return await _pedidoItemRepository.RemoverPedidoItemPorId(id);
    }


}
