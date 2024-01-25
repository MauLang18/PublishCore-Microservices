using AutoMapper;
using PublishCore.Publish.Application.Dtos.Parametro.Request;
using PublishCore.Publish.Application.Dtos.Parametro.Response;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Utilities.Static;

namespace PublishCore.Publish.Application.Mappers
{
    public class ParametroMappingsProfile : Profile
    {
        public ParametroMappingsProfile()
        {
            CreateMap<TbParametro, ParametroResponseDto>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Empresa, x => x.MapFrom(y => y.EmpresaNavigation.Empresa))
                .ForMember(x => x.EstadoParametro, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbParametro>, BaseEntityResponse<ParametroResponseDto>>()
                .ReverseMap();

            CreateMap<ParametroRequestDto, TbParametro>();
        }
    }
}