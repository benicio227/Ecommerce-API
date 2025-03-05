using AutoMapper;
using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.DTOs;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<Pedido, PedidoResponseDto>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total()));

        CreateMap<PedidoCreateDto, Pedido>();


        CreateMap<PedidoItem, PedidoItemResponseDto>();

        CreateMap<PedidoItemCreateDto, PedidoItem>();

    }
}
