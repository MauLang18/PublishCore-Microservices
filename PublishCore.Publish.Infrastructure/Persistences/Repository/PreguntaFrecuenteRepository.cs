using Microsoft.EntityFrameworkCore;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Infrastructure.Persistences.Contexts;
using PublishCore.Publish.Infrastructure.Persistences.Interfaces;

namespace PublishCore.Publish.Infrastructure.Persistences.Repository
{
    public class PreguntaFrecuenteRepository : GenericRepository<TbPreguntaFrecuente>, IPreguntaFrecuenteRepository
    {
        public PreguntaFrecuenteRepository(DbPublishcoreContext context) : base(context) { }

        public async Task<BaseEntityResponse<TbPreguntaFrecuente>> ListPreguntaFrecuente(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbPreguntaFrecuente>();

            var preguntasFrecuentes = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.EmpresaNavigation)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        preguntasFrecuentes = preguntasFrecuentes.Where(x => x.Titulo!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                preguntasFrecuentes = preguntasFrecuentes.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                preguntasFrecuentes = preguntasFrecuentes.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await preguntasFrecuentes.CountAsync();
            response.Items = await Ordering(filters, preguntasFrecuentes, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}