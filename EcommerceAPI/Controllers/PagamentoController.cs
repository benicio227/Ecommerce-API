using AutoMapper;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.DTOs;
using EcommerceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PagamentoController : ControllerBase
{
    private readonly IPagamentoService _pagamentoService;
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMapper _mapper;
    public PagamentoController(
        IPagamentoService pagamentoService,
        IMapper mapper,
        IPedidoRepository pedidoRepository)
    {
        _pagamentoService = pagamentoService;
        _mapper = mapper;
        _pedidoRepository = pedidoRepository;
    }

    [HttpPost]
    public async Task<ActionResult<PagamentoResponseDto>> PostPagamento(PagamentoCreateDto pagamentoCreateDto)
    {
        try
        {
            var pagamento = _mapper.Map<Pagamento>(pagamentoCreateDto);
            var novoPagamento = await _pagamentoService.CriarPagamento(pagamento);
            var responseDto = _mapper.Map<PagamentoResponseDto>(novoPagamento);

            return CreatedAtAction(nameof(GetPagamento), new { id = responseDto.Id }, responseDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno ao processar o pagamento.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PagamentoResponseDto>> GetPagamento(int id)
    {
        var pagamento = await _pagamentoService.ObterPagamentoPorId(id);

        if (pagamento == null)
        {
            return NotFound("Pagamento não encontrado.");
        }

        var responseDto = _mapper.Map<PagamentoResponseDto>(pagamento);
        return Ok(responseDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PagamentoResponseDto>> UpdatePagamento(int id, PagamentoCreateDto pagamentoCreateDto)
    {
     
      var pagamento = await _pagamentoService.EditarPagamento(pagamentoCreateDto);


      
      return NoContent();
    
    }

    [HttpPut("{id}/cancelar")]
    public async Task<ActionResult> CancelarPagamento(int id)
    {
        var sucesso = await _pagamentoService.CancelarPagamento(id);

        if (!sucesso)
        {
            return NotFound("Pagamento não encontrado ou já cancelado.");
        }

        return NoContent();
    }

}
