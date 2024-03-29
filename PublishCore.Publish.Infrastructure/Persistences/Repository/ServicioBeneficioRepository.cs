﻿using Microsoft.EntityFrameworkCore;
using PublishCore.Publish.Domain.Entities;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;
using PublishCore.Publish.Infrastructure.Commons.Bases.Response;
using PublishCore.Publish.Infrastructure.Persistences.Contexts;
using PublishCore.Publish.Infrastructure.Persistences.Interfaces;

namespace PublishCore.Publish.Infrastructure.Persistences.Repository
{
    public class ServicioBeneficioRepository : GenericRepository<TbServicioBeneficio>, IServicioBeneficioRepository
    {
        public ServicioBeneficioRepository(DbPublishcoreContext context) : base(context) { }

        public async Task<BaseEntityResponse<TbServicioBeneficio>> ListServicioBeneficio(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbServicioBeneficio>();

            var parametros = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null && x.EmpresaId == filters.Empresa)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        parametros = parametros.Where(x => x.Titulo!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                parametros = parametros.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                parametros = parametros.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await parametros.CountAsync();
            response.Items = await Ordering(filters, parametros, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}