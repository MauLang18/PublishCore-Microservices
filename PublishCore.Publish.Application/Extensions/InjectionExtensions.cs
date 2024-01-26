using Confluent.Kafka;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublishCore.Publish.Application.Extensions.WatchDog;
using PublishCore.Publish.Application.Interfaces;
using PublishCore.Publish.Application.Services;
using System.Reflection;

namespace PublishCore.Publish.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            /*services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });*/

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IBannerPrincipalApplication, BannerPrincipalApplication>();
            services.AddScoped<IBoletinApplication, BoletinApplication>();
            services.AddScoped<IParametroApplication, ParametroApplication>();
            services.AddScoped<IServicioBeneficioApplication, ServicioBeneficioApplication>();

            var config = new ProducerConfig { BootstrapServers = "broker:29092" };

            services.AddSingleton<IProducer<Null, string>>(x => new ProducerBuilder<Null, string>(config).Build());
            services.AddSingleton<IProducerApplication, ProducerApplication>();

            services.AddWatchDog();

            return services;
        }
    }
}