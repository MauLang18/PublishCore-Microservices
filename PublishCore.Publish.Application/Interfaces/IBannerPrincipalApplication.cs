using PublishCore.Application.Dtos.BannerPrincipal.Response;
using PublishCore.Publish.Application.Commons.Bases;
using PublishCore.Publish.Application.Dtos.BannerPrincipal.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Publish.Application.Interfaces
{
    public interface IBannerPrincipalApplication
    {
        Task<BaseResponse<BaseEntityResponse<BannerPrincipalResponseDto>>> ListBannerPrincipal(BaseFiltersRequest filters);
        Task<BaseResponse<BannerPrincipalResponseDto>> BannerPrincipalById(int id);
        Task<BaseResponse<bool>> RegisterBannerPrincipal(BannerPrincipalRequestDto request);
        Task<BaseResponse<bool>> EditBannerPrincipal(int id, BannerPrincipalRequestDto request);
        Task<BaseResponse<bool>> RemoveBannerPrincipal(int id);
    }
}