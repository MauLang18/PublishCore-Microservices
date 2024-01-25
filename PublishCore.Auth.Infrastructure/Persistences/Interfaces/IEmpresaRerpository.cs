using PublishCore.Auth.Domain.Entities;
using PublishCore.Auth.Infrastructure.Commons.Bases.Request;
using PublishCore.Auth.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Auth.Infrastructure.Persistences.Interfaces
{
    public interface IEmpresaRerpository : IGenericRepository<TbEmpresa>
    {
        Task<BaseEntityResponse<TbEmpresa>> ListEmpresas(BaseFiltersRequest filters);
    }
}