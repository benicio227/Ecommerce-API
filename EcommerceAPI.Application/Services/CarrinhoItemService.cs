using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Application.Services;
public class CarrinhoItemService : ICarrinhoItemService
{
    private readonly ICarrinhoItemRepository _carrinhoItemRepository;

    public CarrinhoItemService(ICarrinhoItemRepository carrinhoItemRepository)
    {
        _carrinhoItemRepository = carrinhoItemRepository;
    }

    public async Task<bool> ExcluirCarrinhoItemPorId(int id)
    {
        var carrinhoItem = await _carrinhoItemRepository.ObterCarrinhoItemPorId(id);
        if (carrinhoItem is null)
        {
            throw new KeyNotFoundException($"CarrinhoItem com ID {id} não encontrado.");
        }

        return await _carrinhoItemRepository.RemoverCarrinhoItemPorId(id);
    }

    public async Task<CarrinhoItem?> ObterCarrinhoItemPorId(int id)
    {
        return await _carrinhoItemRepository.ObterCarrinhoItemPorId(id);
    }
}
