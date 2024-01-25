using Microsoft.EntityFrameworkCore;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Infrastructure.Persistences.Contexts;
using PublishCore.Publish.Infrastructure.Persistences.Interfaces;

namespace PublishCore.Publish.Infrastructure.Persistences.Repository
{
    public class PoliticaRepository : GenericRepository<TbPolitica>, IPoliticaRepository
    {
        public PoliticaRepository(DbPublishcoreContext context) : base(context) { }

        public async Task<BaseEntityResponse<TbPolitica>> ListPolitica(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbPolitica>();

            var politicas = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        politicas = politicas.Where(x => x.Titulo!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                politicas = politicas.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                politicas = politicas.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await politicas.CountAsync();
            response.Items = await Ordering(filters, politicas, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}