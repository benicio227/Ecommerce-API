using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Enums;

namespace EcommerceAPI.Application.Services;
public interface IPedidoService
{
    Task<bool> AtualizarStatusPedido(int pedidoId, StatusPedido novoStatus);
    Task<IEnumerable<Pedido>> ObterPedidos();
    Task<Pedido?> ObterPedidoPorId(int id);
    Task<Pedido?> CriarPedido(int usuarioId);
    Task<bool> EditarPedido(Pedido pedido);
    Task<bool> ExcluirPedidoPorId(int id);
}
