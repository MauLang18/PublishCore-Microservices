using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Publish.Infrastructure.Persistences.Interfaces
{
    public interface IParametroRepository : IGenericRepository<TbParametro>
    {
        Task<BaseEntityResponse<TbParametro>> ListParametro(BaseFiltersRequest filters);
    }
}