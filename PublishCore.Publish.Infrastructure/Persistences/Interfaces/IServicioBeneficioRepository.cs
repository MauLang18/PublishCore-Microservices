using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Publish.Infrastructure.Persistences.Interfaces
{
    public interface IServicioBeneficioRepository : IGenericRepository<TbServicioBeneficio>
    {
        Task<BaseEntityResponse<TbServicioBeneficio>> ListServicioBeneficio(BaseFiltersRequest filters);
    }
}