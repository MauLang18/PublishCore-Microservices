using Microsoft.Extensions.Configuration;
using PublishCore.Publish.Infrastructure.FileStorage;
using PublishCore.Publish.Infrastructure.Persistences.Contexts;
using PublishCore.Publish.Infrastructure.Persistences.Interfaces;

namespace PublishCore.Publish.Infrastructure.Persistences.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbPublishcoreContext _context;
        public IBannerPrincipalRepository BannerPrincipal { get; private set; }

        public IBoletinRepository Boletin { get; private set; }

        public IParametroRepository Parametro { get; private set; }

        public IPoliticaRepository Politica { get; private set; }

        public IPreguntaFrecuenteRepository PreguntaFrecuente { get; private set; }

        public IServicioBeneficioRepository ServicioBeneficio { get; private set; }

        public IServerStorage Storage { get; private set; }

        public UnitOfWork(DbPublishcoreContext context, IConfiguration configuration)
        {
            _context = context;
            BannerPrincipal = new BannerPrincipalRepository(_context);
            Boletin = new BoletinRepository(_context);
            Parametro = new ParametroRepository(_context);
            Politica = new PoliticaRepository(_context);
            PreguntaFrecuente = new PreguntaFrecuenteRepository(_context);
            ServicioBeneficio = new ServicioBeneficioRepository(_context);
            Storage = new ServerStorage(configuration);
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