using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Services;
public interface ICarrinhoItemService
{
    Task<CarrinhoItem?> ObterCarrinhoItemPorId(int id);
    Task<bool> ExcluirCarrinhoItemPorId(int id);
}
