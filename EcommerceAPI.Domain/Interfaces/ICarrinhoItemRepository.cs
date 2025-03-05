using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Repositories;

public interface ICarrinhoItemRepository
{
    Task<CarrinhoItem?> ObterCarrinhoItemPorId(int id);
    Task<bool> RemoverCarrinhoItemPorId(int id);
}
