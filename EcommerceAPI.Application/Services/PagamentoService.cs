using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Enums;
using EcommerceAPI.DTOs;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Application.Services;
public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IPedidoRepository _pedidoRepository;
    public PagamentoService(IPagamentoRepository pagamentoRepository, IPedidoRepository pedidoRepository)
    {
        _pagamentoRepository = pagamentoRepository;
        _pedidoRepository = pedidoRepository;
    }
    public async Task<IEnumerable<Pagamento>> ObterPagamentos(int id)
    {
        return await _pagamentoRepository.ObterPagamentos(id);
    }
    public async Task<Pagamento?> ObterPagamentoPorId(int id)
    {
        return await _pagamentoRepository.ObterPagamentoPorId(id);
    }
    public async Task<Pagamento> CriarPagamento(Pagamento pagamento)
    {
        var pedido = await _pedidoRepository.ObterPedidoPorId(pagamento.PedidoId);
        if (pedido == null)
        {
            throw new InvalidOperationException("Pedido não encontrado.");
        }

        decimal valorTotal = pedido.PedidoItems.Sum(item => item.Quantidade * item.PrecoUnitario);

        pagamento.Valor = valorTotal;
        pagamento.Status = StatusPagamento.Aprovado;
        pagamento.DataPagamento = DateTime.UtcNow;

        return await _pagamentoRepository.AdicionarPagamento(pagamento);
    }


    public async Task<bool> EditarPagamento(PagamentoCreateDto pagamento)
    {
        var pagamentoExistente = await _pagamentoRepository.ObterPagamentoPorId(pagamento.Id);

        if (pagamentoExistente is null)
        {
            throw new InvalidOperationException("Pagamento não encontrado.");
        }

        pagamentoExistente.Valor = pagamento.Valor;
        pagamentoExistente.Metodo = pagamento.Metodo;

        return await _pagamentoRepository.SalvarAlteracoesPagamento(pagamentoExistente);
    }

    public async Task<bool> ExcluirPagamentoPorId(int id)
    {
        return await _pagamentoRepository.RemoverPagamentoPorId(id);
    }
    public async Task<bool> CancelarPagamento(int id)
    {
        var pagamento = await _pagamentoRepository.ObterPagamentoPorId(id);
        if (pagamento is null)
        {
            return false;
        }

        pagamento.Status = StatusPagamento.Cancelado;

        await _pagamentoRepository.SalvarAlteracoesPagamento(pagamento);
        return true;
    }

}
