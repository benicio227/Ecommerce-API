using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Repositories;

public interface IPedidoItemRepository
{
    Task<IEnumerable<PedidoItem>> ObterPedidosItem();
    Task<PedidoItem?> ObterPedidoItemPorId(int id);
    Task<PedidoItem?> AdicionarPedidoItem(PedidoItem pedidoItem);
    Task<bool> SalvarAlteracoesPedidoItem(PedidoItem pedidoItem);
    Task<bool> RemoverPedidoItemPorId(int id);
}
