using Microsoft.EntityFrameworkCore;
using PublishCore.Auth.Domain.Entities;
using PublishCore.Auth.Infrastructure.Commons.Bases.Request;
using PublishCore.Auth.Infrastructure.Commons.Bases.Response;
using PublishCore.Auth.Infrastructure.Persistences.Contexts;
using PublishCore.Auth.Infrastructure.Persistences.Interfaces;

namespace PublishCore.Auth.Infrastructure.Persistences.Repository
{
    public class EmpresaRepository : GenericRepository<TbEmpresa>, IEmpresaRerpository
    {
        private readonly DbPublishcoreContext _context;
        public EmpresaRepository(DbPublishcoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<TbEmpresa>> ListEmpresas(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbEmpresa>();

            var empresas = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        empresas = empresas.Where(x => x.Empresa!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                empresas = empresas.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                empresas = empresas.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await empresas.CountAsync();
            response.Items = await Ordering(filters, empresas, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}