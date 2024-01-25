using Microsoft.EntityFrameworkCore;
using PublishCore.Auth.Domain.Entities;
using PublishCore.Auth.Infrastructure.Commons.Bases.Request;
using PublishCore.Auth.Infrastructure.Commons.Bases.Response;
using PublishCore.Auth.Infrastructure.Persistences.Contexts;
using PublishCore.Auth.Infrastructure.Persistences.Interfaces;
using PublishCore.Auth.Utilities.Static;

namespace PublishCore.Auth.Infrastructure.Persistences.Repository
{
    public class UsuarioRepository : GenericRepository<TbUsuario>, IUsuarioRepository
    {
        private readonly DbPublishcoreContext _context;
        public UsuarioRepository(DbPublishcoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<TbUsuario>> ListUsuarios(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbUsuario>();

            var usuarios = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        usuarios = usuarios.Where(x => x.Usuario!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                usuarios = usuarios.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                usuarios = usuarios.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await usuarios.CountAsync();
            response.Items = await Ordering(filters, usuarios, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<TbUsuario> UserByUser(string users)
        {
            var user = await _context.TbUsuarios.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Usuario!.Equals(users) && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null && x.Estado.Equals((int)StateTypes.Activo));

            return user!;
        }
    }
}