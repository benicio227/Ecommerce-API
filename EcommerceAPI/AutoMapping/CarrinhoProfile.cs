using AutoMapper;
using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.DTOs;

public class CarrinhoProfile : Profile
{
    public CarrinhoProfile()
    {
        CreateMap<Carrinho, CarrinhoResponseDto>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

        CreateMap<CarrinhoCreateDto, Carrinho>();

        CreateMap<CarrinhoItem, CarrinhoItemResponseDto>();
        CreateMap<CarrinhoItemCreateDto, CarrinhoItem>();
    }
}
