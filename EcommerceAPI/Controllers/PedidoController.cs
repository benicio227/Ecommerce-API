using AutoMapper;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;
    private readonly IMapper _mapper;
    public PedidoController(IPedidoService pedidoService, IMapper mapper)
    {
        _pedidoService = pedidoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoResponseDto>>> GetPedidos()
    {
        var pedidos = await _pedidoService.ObterPedidos();

        if (!pedidos.Any())
        {
            return NoContent();
        }

        var pedidosDto = _mapper.Map<IEnumerable<PedidoResponseDto>>(pedidos);

        return Ok(pedidosDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoResponseDto>> GetPedido(int id)
    {
        var pedido = await _pedidoService.ObterPedidoPorId(id);

        if (pedido is null)
        {
            return NotFound("Pedido não encontrado");
        }

        var pedidoDto = _mapper.Map<PedidoResponseDto>(pedido);

        return Ok(pedidoDto);
    }

    [HttpPost]
    public async Task<ActionResult<PedidoResponseDto>> PostPedido(PedidoCreateDto pedidoCreateDto)
    {
        try
        {
            var pedido = _mapper.Map<Pedido>(pedidoCreateDto);
            await _pedidoService.CriarPedido(pedido.UsuarioId);

            if (pedido == null)
            {
                return BadRequest("Erro ao criar pedido. O carrinho pode estar vazio.");
            }

            var pedidoResponseDto = _mapper.Map<PedidoResponseDto>(pedido);
            return CreatedAtAction(nameof(GetPedido), new { id = pedidoResponseDto.Id }, pedidoResponseDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePedido(int id, PedidoCreateDto pedidoCreateDto)
    {
        if (id != pedidoCreateDto.Id)
        {
            return BadRequest("O ID do pedido não corresponde ao ID fornecido na URL");
        }

        var sucesso = await _pedidoService.EditarPedido(_mapper.Map<Pedido>(pedidoCreateDto));

        if (!sucesso)
        {
            return NotFound("Pedido não encontrado.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePedido(int id)
    {
        var sucesso = await _pedidoService.ExcluirPedidoPorId(id);
        if (!sucesso)
        {
            return NotFound("Pedido não encontrado");
        }

        return NoContent();
    }
}
