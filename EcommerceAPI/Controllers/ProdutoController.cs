using AutoMapper;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.DTOs;
using EcommerceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;
    public ProdutoController(IProdutoService produtoService, IMapper mapper, ICategoriaRepository categoriaRepository)
    {
        _produtoService = produtoService;
        _mapper = mapper;
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoResponseDto>>> GetProdutos()
    {
        var produtos = await _produtoService.ObterProdutos();
        var produtoDtos = _mapper.Map<IEnumerable<ProdutoResponseDto>>(produtos);
        return Ok(produtoDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoResponseDto>> GetProduto(int id)
    {
        try
        {
            var produto = await _produtoService.ObterProdutoPorId(id);
            var produtoDto = _mapper.Map<ProdutoResponseDto>(produto);
            return Ok(produtoDto);
        }
        catch(KeyNotFoundException)
        {
            return NotFound("Produto não encontrado");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoResponseDto>> PostProduto(ProdutoCreateDto produtoCreateDto)
    {

        try
        {
            var produto = _mapper.Map<Produto>(produtoCreateDto);

            await _produtoService.CriarProduto(produto);

            var produtoResponseDto = _mapper.Map<ProdutoResponseDto>(produto);

            return CreatedAtAction(nameof(GetProduto), new { id = produtoResponseDto.Id}, produtoResponseDto);

        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduto(int id, ProdutoCreateDto produtoCreateDto)
    {

        if (id != produtoCreateDto.Id)
        {
            return BadRequest("O ID do produto não corresponde ao ID fornecido na URL");
        }

        try
        {
            var produto = _mapper.Map<Produto>(produtoCreateDto);
            await _produtoService.EditarProduto(produto);
            return NoContent();
        }
        catch(KeyNotFoundException)
        {
            return NotFound("Porduto não encontrado");
        }
      
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduto(int id)
    {
        try
        {
            await _produtoService.ExcluirProdutoPorId(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Produto não encontrado");
        }
    }
}
