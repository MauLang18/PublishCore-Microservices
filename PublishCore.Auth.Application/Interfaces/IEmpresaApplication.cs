using PublishCore.Auth.Application.Commons.Bases;
using PublishCore.Auth.Application.Dtos.Empresa.Request;
using PublishCore.Auth.Application.Dtos.Empresa.Response;
using PublishCore.Auth.Infrastructure.Commons.Bases.Request;
using PublishCore.Auth.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Auth.Application.Interfaces
{
    public interface IEmpresaApplication
    {
        Task<BaseResponse<BaseEntityResponse<EmpresaResponseDto>>> ListEmpresas(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<EmpresaSelectResponseDto>>> ListSelectEmpresa();
        Task<BaseResponse<EmpresaResponseDto>> EmpresaById(int id);
        Task<BaseResponse<bool>> RegisterEmpresa(EmpresaRequestDto requestDto);
        Task<BaseResponse<bool>> EditEmpresa(int id, EmpresaRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveEmpresa(int id);
    }
}