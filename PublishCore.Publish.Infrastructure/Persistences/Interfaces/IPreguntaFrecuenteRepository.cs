using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;

namespace PublishCore.Publish.Infrastructure.Persistences.Interfaces
{
    public interface IPreguntaFrecuenteRepository : IGenericRepository<TbPreguntaFrecuente>
    {
        Task<BaseEntityResponse<TbPreguntaFrecuente>> ListPreguntaFrecuente(BaseFiltersRequest filters);
    }
}