using AutoMapper;
using PublishCore.Auth.Application.Dtos.Empresa.Request;
using PublishCore.Auth.Application.Dtos.Empresa.Response;
using PublishCore.Auth.Domain.Entities;
using PublishCore.Auth.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Auth.Application.Mappers
{
    public class EmpresaMappingProfile : Profile
    {
        public EmpresaMappingProfile()
        {
            CreateMap<TbEmpresa, EmpresaResponseDto>()
                .ReverseMap();
            CreateMap<BaseEntityResponse<TbEmpresa>, BaseEntityResponse<EmpresaResponseDto>>()
                .ReverseMap();
            CreateMap<EmpresaRequestDto, TbEmpresa>()
                .ReverseMap();
            CreateMap<TbEmpresa, EmpresaSelectResponseDto>()
                .ReverseMap();
        }
    }
}