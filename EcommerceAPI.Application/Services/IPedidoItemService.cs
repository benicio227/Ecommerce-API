using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Services;
public interface IPedidoItemService
{
    Task<PedidoItem?> ObterPedidoItemPorId(int id);
    Task<IEnumerable<PedidoItem>> ObterPedidosItem();
    Task<PedidoItem?> CriarPedidoItem(PedidoItem pedidoItem);
    Task<bool> ExcluirPedidoItemPorId(int id);
}
