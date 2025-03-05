using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Enums;

namespace EcommerceAPI.Repositories;

public interface IPedidoRepository
{
    Task<IEnumerable<Pedido>> ObterPedidos();
    Task<Pedido?> ObterPedidoPorId(int id);
    Task AdicionarPedido(Pedido pedido);
    Task<bool> SalvarAlteracoesPedido(Pedido pedido);
    Task<bool> RemoverPedidoPorId(int id);
    Task<Pedido?> BuscarPedidoPorId(int id);
}
