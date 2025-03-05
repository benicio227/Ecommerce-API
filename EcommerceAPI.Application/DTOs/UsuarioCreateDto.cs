﻿using EcommerceAPI.Domain.Enums;

namespace EcommerceAPI.DTOs;

public class UsuarioCreateDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email {  get; set; } = string.Empty;
    public string Senha {  get; set; } = string.Empty;
    public PerfilUsuario Perfil { get; set; } = PerfilUsuario.Cliente;
    public string? Telefone {  get; set; }
}
