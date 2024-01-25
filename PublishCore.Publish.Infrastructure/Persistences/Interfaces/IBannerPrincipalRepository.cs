using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Publish.Infrastructure.Persistences.Interfaces
{
    public interface IBannerPrincipalRepository : IGenericRepository<TbBannerPrincipal>
    {
        Task<BaseEntityResponse<TbBannerPrincipal>> ListBannerPrincipal(BaseFiltersRequest filters);
    }
}