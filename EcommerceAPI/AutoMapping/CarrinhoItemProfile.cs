using AutoMapper;
using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.DTOs;

public class CarrinhoItemProfile : Profile
{
    public CarrinhoItemProfile()
    {
        CreateMap<CarrinhoItem, CarrinhoItemResponseDto>();

        
        CreateMap<CarrinhoItemCreateDto, CarrinhoItem>();
    }
}
