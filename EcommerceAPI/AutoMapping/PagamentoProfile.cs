using AutoMapper;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Enums;

namespace EcommerceAPI.DTOs;

public class PagamentoProfile : Profile
{
    public PagamentoProfile()
    {
        CreateMap<Pagamento, PagamentoResponseDto>();

        CreateMap<PagamentoCreateDto, Pagamento>()
            .ForMember(dest => dest.DataPagamento, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)StatusPagamento.Pendente));
    }
}
