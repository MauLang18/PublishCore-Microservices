using Microsoft.EntityFrameworkCore;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Infrastructure.Persistences.Contexts;
using PublishCore.Publish.Infrastructure.Persistences.Interfaces;

namespace PublishCore.Publish.Infrastructure.Persistences.Repository
{
    public class BannerPrincipalRepository : GenericRepository<TbBannerPrincipal>, IBannerPrincipalRepository
    {
        public BannerPrincipalRepository(DbPublishcoreContext context) : base(context) { }

        public async Task<BaseEntityResponse<TbBannerPrincipal>> ListBannerPrincipal(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbBannerPrincipal>();

            var bannerPrincipal = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null && x.EmpresaId == filters.Empresa)
                .Include(x => x.EmpresaNavigation)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        bannerPrincipal = bannerPrincipal.Where(x => x.Nombre!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                bannerPrincipal = bannerPrincipal.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                bannerPrincipal = bannerPrincipal.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await bannerPrincipal.CountAsync();
            response.Items = await Ordering(filters, bannerPrincipal, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}