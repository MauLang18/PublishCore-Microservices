using AutoMapper;
using PublishCore.Publish.Application.Dtos.Boletin.Request;
using PublishCore.Publish.Application.Dtos.Boletin.Response;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Utilities.Static;

namespace PublishCore.Publish.Application.Mappers
{
    public class BoletinMappingsProfile : Profile
    {
        public BoletinMappingsProfile()
        {
            CreateMap<TbBoletin, BoletinResponseDto>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Empresa, x => x.MapFrom(y => y.EmpresaNavigation.Empresa))
                .ForMember(x => x.EstadoBoletin, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ForMember(x => x.ProgramacionBoletin, x => x.MapFrom(y => y.Programacion.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbBoletin>, BaseEntityResponse<BoletinResponseDto>>()
                .ReverseMap();

            CreateMap<BoletinRequestDto, TbBoletin>();
        }
    }
}