using Microsoft.EntityFrameworkCore;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Infrastructure.Persistences.Contexts;
using PublishCore.Publish.Infrastructure.Persistences.Interfaces;

namespace PublishCore.Publish.Infrastructure.Persistences.Repository
{
    public class BoletinRepository : GenericRepository<TbBoletin>, IBoletinRepository
    {
        public BoletinRepository(DbPublishcoreContext context) : base(context) { }

        public async Task<BaseEntityResponse<TbBoletin>> ListBoletin(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbBoletin>();

            var boletin = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null && x.EmpresaId == filters.Empresa)
                .Include(x => x.EmpresaNavigation)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        boletin = boletin.Where(x => x.Nombre!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                boletin = boletin.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                boletin = boletin.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await boletin.CountAsync();
            response.Items = await Ordering(filters, boletin, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}