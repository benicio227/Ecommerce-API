using AutoMapper;
using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.DTOs;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<Categoria, CategoriaResponseDto>();

        CreateMap<CategoriaCreateDto, Categoria>();
    }
}
