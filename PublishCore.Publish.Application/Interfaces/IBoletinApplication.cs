using PublishCore.Publish.Application.Commons.Bases;
using PublishCore.Publish.Application.Dtos.Boletin.Request;
using PublishCore.Publish.Application.Dtos.Boletin.Response;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Publish.Application.Interfaces
{
    public interface IBoletinApplication
    {
        Task<BaseResponse<BaseEntityResponse<BoletinResponseDto>>> ListBoletin(BaseFiltersRequest filters);
        Task<BaseResponse<BoletinResponseDto>> BoletinById(int id);
        Task<BaseResponse<bool>> RegisterBoletin(BoletinRequestDto requestDto);
        Task<BaseResponse<bool>> EditBoletin(int id, BoletinRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveBoletin(int id);
    }
}