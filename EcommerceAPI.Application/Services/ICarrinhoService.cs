using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Services;
public interface ICarrinhoService
{
    Task<Carrinho?> ObterCarrinhoPorUsuarioId(int usuarioId);
    Task<Carrinho?> CriarCarrinho(int usuarioId);
    Task<bool> ExcluirCarrinhoPorId(int id);
    Task<CarrinhoItem?> AdicionarItemAoCarrinho(int carrinhoId, int produtoId, int quantidade);
    Task LimparCarrinho(int usuarioId);
}
