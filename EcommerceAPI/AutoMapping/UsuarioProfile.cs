using AutoMapper;
using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.DTOs;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioResponseDto>().ReverseMap();
        CreateMap<UsuarioCreateDto, Usuario>()
            .ForMember(dest => dest.SenhaHash, opt => opt.MapFrom(src => src.Senha));
        CreateMap<UsuarioUpdateDto, Usuario>()
               .ForMember(dest => dest.SenhaHash, opt => opt.MapFrom(src => src.Senha));
    }
}
