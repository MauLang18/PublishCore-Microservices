using PublishCore.Publish.Application.Commons.Bases;
using PublishCore.Publish.Application.Dtos.Parametro.Request;
using PublishCore.Publish.Application.Dtos.Parametro.Response;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Publish.Application.Interfaces
{
    public interface IParametroApplication
    {
        Task<BaseResponse<BaseEntityResponse<ParametroResponseDto>>> ListParametros(BaseFiltersRequest filters);
        Task<BaseResponse<ParametroResponseDto>> ParametroById(int id);
        Task<BaseResponse<bool>> RegisterParametro(ParametroRequestDto requestDto);
        Task<BaseResponse<bool>> EditParametro(int id, ParametroRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveParametro(int id);
    }
}