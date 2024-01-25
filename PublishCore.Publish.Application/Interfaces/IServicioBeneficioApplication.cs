using PublishCore.Publish.Application.Commons.Bases;
using PublishCore.Publish.Application.Dtos.ServicioBeneficio.Request;
using PublishCore.Publish.Application.Dtos.ServicioBeneficio.Response;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Publish.Application.Interfaces
{
    public interface IServicioBeneficioApplication
    {
        Task<BaseResponse<BaseEntityResponse<ServicioBeneficioResponseDto>>> ListServicioBeneficio(BaseFiltersRequest filters);
        Task<BaseResponse<ServicioBeneficioResponseDto>> ServivioBeneficioById(int id);
        Task<BaseResponse<bool>> RegisterServicioBeneficio(ServicioBeneficioRequestDto requestDto);
        Task<BaseResponse<bool>> EditServicioBeneficio(int id, ServicioBeneficioRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveServicioBeneficio(int id);
    }
}