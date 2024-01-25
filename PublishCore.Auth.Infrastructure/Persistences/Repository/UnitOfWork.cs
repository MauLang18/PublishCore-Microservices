using Microsoft.Extensions.Configuration;
using PublishCore.Auth.Infrastructure.Persistences.Contexts;
using PublishCore.Auth.Infrastructure.Persistences.Interfaces;

namespace PublishCore.Auth.Infrastructure.Persistences.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbPublishcoreContext _context;

        public IUsuarioRepository Usuario { get; private set; }

        public IEmpresaRerpository Empresa {  get; private set; }

        public UnitOfWork(DbPublishcoreContext context, IConfiguration configuration)
        {
            _context = context;
            Usuario = new UsuarioRepository(_context);
            Empresa = new EmpresaRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}