using AutoMapper;
using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.DTOs;

public class PedidoItemProfile : Profile
{
    public PedidoItemProfile()
    {
        CreateMap<PedidoItem, PedidoItemResponseDto>();

        CreateMap<PedidoItemCreateDto, PedidoItem>();
    }
}
