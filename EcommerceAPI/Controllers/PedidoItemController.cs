using AutoMapper;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.DTOs;
using EcommerceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PedidoItemController : ControllerBase
{
    private readonly IPedidoItemService _pedidoItemService;
    private readonly IMapper _mapper;

    public PedidoItemController(IPedidoItemService pedidoItemService, IMapper mapper)
    {
        _pedidoItemService = pedidoItemService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoItemResponseDto>> GetPedidoItem(int id)
    {
        var pedidoItem = await _pedidoItemService.ObterPedidoItemPorId(id);

        if (pedidoItem is null)
        {
            return NotFound("PedidoItem não encontrado");
        }

        var pedidoItemDto = _mapper.Map<PedidoItemResponseDto>(pedidoItem); 

        return Ok(pedidoItemDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoItemResponseDto>>> GetPedidosItems()
    {
        var pedidoItens = await _pedidoItemService.ObterPedidosItem();

        if (!pedidoItens.Any())
        {
            return NoContent();
        }


        var pedidoItensDto = _mapper.Map<IEnumerable<PedidoItemResponseDto>>(pedidoItens);
        return Ok(pedidoItensDto);
    }


    [HttpPost]
    public async Task<ActionResult<PedidoItemResponseDto>> PostPedidoItem(PedidoItemCreateDto pedidoItemCreateDto)
    {
        if (pedidoItemCreateDto == null)
        {
            return BadRequest("Dados inválidos");
        }

        var pedidoItem = _mapper.Map<PedidoItem>(pedidoItemCreateDto);
        await _pedidoItemService.CriarPedidoItem(pedidoItem);
 
        var responseDto = _mapper.Map<PedidoItemResponseDto>(pedidoItem);

        return CreatedAtAction(nameof(GetPedidoItem), new { id = responseDto.Id }, responseDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePedidoItem(int id)
    {
        
        var sucesso = await _pedidoItemService.ExcluirPedidoItemPorId(id);


        if (!sucesso)
        {
            return NotFound();
        }

        return NoContent();
    }
}
