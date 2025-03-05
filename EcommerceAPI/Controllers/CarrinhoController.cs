using AutoMapper;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CarrinhoController : ControllerBase
{
    private readonly ICarrinhoService _carrinhoService;
    private readonly IMapper _mapper;   
    public CarrinhoController(ICarrinhoService carrinhoService, IMapper mapper)
    {
        _carrinhoService = carrinhoService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarrinhoResponseDto>> GetCarrinho(int id)
    {
        var carrinho = await _carrinhoService.ObterCarrinhoPorUsuarioId(id);
        if (carrinho is null)
        {
            return NotFound("Carrinho não encontrado");
        }

        var responseDto = _mapper.Map<CarrinhoResponseDto>(carrinho);
        return Ok(responseDto);
    }

    [HttpPost("{carrinhoId}/itens")]
    public async Task<ActionResult<CarrinhoItemResponseDto>> AdicionarItemAoCarrinho(int carrinhoId, [FromBody] CarrinhoItemCreateDto carrinhoItemCreateDto)
    {

        if (carrinhoItemCreateDto == null)
        {
            return BadRequest("Dados do item inválidos.");
        }

        var novoItem = await _carrinhoService.AdicionarItemAoCarrinho(
            carrinhoId, carrinhoItemCreateDto.ProdutoId, carrinhoItemCreateDto.Quantidade);

        var responseDto = _mapper.Map<CarrinhoItemResponseDto>(novoItem);
        return CreatedAtAction(nameof(GetCarrinho), new { id = carrinhoId }, responseDto);

    }


    [HttpPost]
    public async Task<ActionResult<CarrinhoResponseDto>> PostCarrinho(CarrinhoCreateDto carrinhoCreateDto)
    {

        var carrinho = _mapper.Map<Carrinho>(carrinhoCreateDto);

        var carrinhoCriado = await _carrinhoService.CriarCarrinho(carrinho.UsuarioId);

        if (carrinhoCriado is null)
        {
            return StatusCode(500, "Erro ao criar o carrinho.");
        }

        var responseDto = _mapper.Map<CarrinhoResponseDto>(carrinhoCriado);
        return CreatedAtAction(nameof(GetCarrinho), new { id = responseDto.Id }, responseDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCarrinho(int id)
    {
        var carrinhoRemovido = await _carrinhoService.ExcluirCarrinhoPorId(id);
        if (!carrinhoRemovido)
        {
            return NotFound(new { mensagem = "Carrinho não encontrado" });
        }

        return NoContent();
    }
}
