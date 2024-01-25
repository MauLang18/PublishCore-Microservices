using AutoMapper;
using PublishCore.Publish.Application.Dtos.ServicioBeneficio.Request;
using PublishCore.Publish.Application.Dtos.ServicioBeneficio.Response;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Utilities.Static;

namespace PublishCore.Publish.Application.Mappers
{
    public class ServicioBeneficioMappingProfile : Profile
    {
        public ServicioBeneficioMappingProfile()
        {
            CreateMap<TbServicioBeneficio, ServicioBeneficioResponseDto>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Empresa, x => x.MapFrom(y => y.EmpresaNavigation.Empresa))
                .ForMember(x => x.EstadoServicioBeneficio, x => x.MapFrom(y => y.Estado.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ForMember(x => x.ProgramacionServicioBeneficio, x => x.MapFrom(y => y.Programacion.Equals((int)StateTypes.Activo) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<TbServicioBeneficio>, BaseEntityResponse<ServicioBeneficioResponseDto>>()
                .ReverseMap();

            CreateMap<ServicioBeneficioRequestDto, TbServicioBeneficio>();
        }
    }
}