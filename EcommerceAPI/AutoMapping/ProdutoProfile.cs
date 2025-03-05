using AutoMapper;
using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.DTOs;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoResponseDto>();

        CreateMap<ProdutoCreateDto, Produto>();
    }
}
