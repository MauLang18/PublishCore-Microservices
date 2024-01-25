using PublishCore.Auth.Domain.Entities;
using PublishCore.Auth.Infrastructure.Commons.Bases.Request;
using PublishCore.Auth.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Auth.Infrastructure.Persistences.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<TbUsuario>
    {
        Task<BaseEntityResponse<TbUsuario>> ListUsuarios(BaseFiltersRequest filters);
        Task<TbUsuario> UserByUser(string user);
    }
}