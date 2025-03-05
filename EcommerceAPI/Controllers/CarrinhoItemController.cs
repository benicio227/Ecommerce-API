using AutoMapper;
using EcommerceAPI.DTOs;
using EcommerceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CarrinhoItemController : ControllerBase
{
    private readonly ICarrinhoItemRepository _carrinhoItemRepository;
    private readonly ICarrinhoRepository _carrinhoRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    public CarrinhoItemController(
        ICarrinhoItemRepository carrinhoItemRepository,
        IMapper mapper,
        ICarrinhoRepository carrinhoRepository,
        IProdutoRepository produtoRepository)
    {
        _carrinhoItemRepository = carrinhoItemRepository;
        _carrinhoRepository = carrinhoRepository;
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarrinhoItemResponseDto>> GetCarrinhoItem(int id)
    {
        var carrinhoItem = await _carrinhoItemRepository.ObterCarrinhoItemPorId(id);

        if (carrinhoItem is null)
        {
            return NotFound("CarrinhoItem não encontrado");
        }

        var responseDto = _mapper.Map<CarrinhoItemResponseDto>(carrinhoItem);

        return Ok(responseDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCarrinhoItem(int id)
    {
        var itemExcluido = await _carrinhoItemRepository.RemoverCarrinhoItemPorId(id);
        if (!itemExcluido)
        {
            return NotFound(new { mensagem = $"CarrinhoItem com ID {id} não encontrado ou já foi excluído" });
        }

        return NoContent();
    }
}
