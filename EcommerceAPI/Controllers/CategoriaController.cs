using AutoMapper;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;
    private readonly IMapper _mapper;
    public CategoriaController(ICategoriaService categoriaService, IMapper mapper)
    {
        _categoriaService = categoriaService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponseDto>>> GetCategorias()
    {
        var categorias = await _categoriaService.ObterCategorias();

        var categoriasDto = _mapper.Map<IEnumerable<CategoriaResponseDto>>(categorias);

        return Ok(categoriasDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponseDto>> GetCategoria(int id)
    {
        var categoria = await _categoriaService.ObterCategoriaPorId(id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrado");
        }

        var categoriaDto = _mapper.Map<CategoriaResponseDto>(categoria);

        return Ok(categoriaDto);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaResponseDto>> PostCategoria(CategoriaCreateDto categoriaCreateDto)
    {
        var categoria = _mapper.Map<Categoria>(categoriaCreateDto);

        try
        {
            await _categoriaService.CriarCategoria(categoria);

            var categoriaResponseDto = _mapper.Map<CategoriaResponseDto>(categoria);

            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoriaResponseDto);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategoria(int id, CategoriaCreateDto categoriaCreateDto)
    {
        var categoria = _mapper.Map<Categoria>(categoriaCreateDto);
        categoria.Id = id;

        var sucesso = await _categoriaService.EditarCategoria(categoria);

        if (!sucesso)
        {
            return NotFound($"Categoria com o ID {id} não encontrada ou já possui esse nome.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategoria(int id)
    {
        var sucesso = await _categoriaService.ExcluirCategoriaPorId(id);

        if (!sucesso)
        {
            return NotFound($"Categoria com o ID {id} não encontrada");
        }

        return NoContent();
    }
}
