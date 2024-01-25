using PublishCore.Publish.Infrastructure.FileStorage;

namespace PublishCore.Publish.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaracion o matricula de nuestra interfaces a nivel de repository

        IBannerPrincipalRepository BannerPrincipal { get; }
        IBoletinRepository Boletin { get; }
        IParametroRepository Parametro { get; }
        IPoliticaRepository Politica { get; }
        IPreguntaFrecuenteRepository PreguntaFrecuente { get; }
        IServicioBeneficioRepository ServicioBeneficio { get; }
        IServerStorage Storage { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}