using AutoMapper;
using PublishCore.Auth.Application.Dtos.Usuario.Request;
using PublishCore.Auth.Application.Dtos.Usuario.Response;
using PublishCore.Auth.Domain.Entities;
using PublishCore.Auth.Infrastructure.Commons.Bases.Response;
using PublishCore.Auth.Utilities.Static;

namespace PublishCore.Auth.Application.Mappers
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<TbUsuario, UsuarioResponseDto>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.EstadoUsuario, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbUsuario>, BaseEntityResponse<UsuarioResponseDto>>()
                .ReverseMap();

            CreateMap<UsuarioRequestDto, TbUsuario>();

            CreateMap<TokenRequestDto, TbUsuario>();
        }
    }
}