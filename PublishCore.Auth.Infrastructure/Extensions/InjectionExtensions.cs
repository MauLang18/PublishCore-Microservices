using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublishCore.Auth.Infrastructure.Persistences.Contexts;
using PublishCore.Auth.Infrastructure.Persistences.Interfaces;
using PublishCore.Auth.Infrastructure.Persistences.Repository;

namespace PublishCore.Auth.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DbPublishcoreContext).Assembly.FullName;

            services.AddDbContext<DbPublishcoreContext>(
                options => options.UseSqlServer(
                       configuration.GetConnectionString("Connection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}