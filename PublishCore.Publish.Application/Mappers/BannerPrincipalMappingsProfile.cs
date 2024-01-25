using AutoMapper;
using PublishCore.Application.Dtos.BannerPrincipal.Response;
using PublishCore.Publish.Application.Dtos.BannerPrincipal.Request;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Utilities.Static;

namespace PublishCore.Publish.Application.Mappers
{
    public class BannerPrincipalMappingsProfile : Profile
    {
        public BannerPrincipalMappingsProfile()
        {
            CreateMap<TbBannerPrincipal, BannerPrincipalResponseDto>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Empresa, x => x.MapFrom(y => y.EmpresaNavigation.Empresa))
                .ForMember(x => x.EstadoBannerPrincipal, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ForMember(x => x.ProgramacionBannerPrincipal, x => x.MapFrom(y => y.Programacion.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbBannerPrincipal>, BaseEntityResponse<BannerPrincipalResponseDto>>()
                .ReverseMap();

            CreateMap<BannerPrincipalRequestDto, TbBannerPrincipal>();
        }
    }
}