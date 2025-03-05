using AutoMapper;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IMapper _mapper;
    public UsuarioController(
        IUsuarioService usuarioService,
        IMapper mapper)
    {
        _usuarioService = usuarioService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetUsuarios()
    {
        var usuarios = await _usuarioService.ObterUsuarios();

        if (!usuarios.Any())
        {
            return NoContent();
        }

        var usuarioDtos = _mapper.Map<IEnumerable<UsuarioResponseDto>>(usuarios);

        return Ok(usuarioDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioResponseDto>> GetUsuario(int id)
    {
        var usuario = await _usuarioService.ObterUsuarioPorId(id);

        if (usuario is null)
        {
            return NotFound("Usuario não encontrado");
        }

        var usuarioDto = _mapper.Map<UsuarioResponseDto>(usuario);

        return Ok(usuarioDto);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDto>> PostUsuario(UsuarioCreateDto usuarioCreateDto)
    {
        var usuario = _mapper.Map<Usuario>(usuarioCreateDto);

        var usuarioCriado = await _usuarioService.CriarUsuario(usuario);

        var usuarioResponseDto = _mapper.Map<UsuarioResponseDto>(usuarioCriado);

        return CreatedAtAction(nameof(GetUsuario), new { id = usuarioResponseDto.Id }, usuarioResponseDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUsuario(int id, UsuarioUpdateDto usuarioUpdateDto)
    {

        if (id != usuarioUpdateDto.Id)
        {
            return BadRequest("O ID do usuario não corresponde ao ID fornecido na URL");
        }

        try
        {
            await _usuarioService.EditarUsuario(_mapper.Map<Usuario>(usuarioUpdateDto));

            return NoContent();
        }
        catch(KeyNotFoundException)
        {
            return NotFound("Usuário não encontrado");
        }
    
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUsuario(int id)
    {
        var usuarioExistente = await _usuarioService.ObterUsuarioPorId(id);
        if (usuarioExistente is null)
        {
            return NotFound("Usuário não encontrado");
        }

        await _usuarioService.DeletarUsuarioPorId(id);
        return NoContent();
    }
}
