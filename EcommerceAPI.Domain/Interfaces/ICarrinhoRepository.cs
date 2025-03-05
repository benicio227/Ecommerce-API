using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Repositories;

public interface ICarrinhoRepository
{
    Task<Carrinho?> ObterCarrinhoPorUsuarioId(int usuarioId);

    Task<Carrinho?> ObterCarrinhoPorId(int carrinhoId);

    Task<Carrinho?> AdicionarCarrinho(Carrinho carrinho);

    Task<bool> SalvarAlteracoesCarrinho(Carrinho carrinho);
    Task<bool> RemoverCarrinhoPorId(int id);
}
