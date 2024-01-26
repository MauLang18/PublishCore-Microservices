using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublishCore.Hubs.Application.HostedServices;
using PublishCore.Hubs.Application.Interfaces;
using PublishCore.Hubs.Application.Services;

namespace PublishCore.Hubs.Application.Extensions
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

            var config = new ConsumerConfig {
                GroupId = "publishCore-consumer-group",
                BootstrapServers = "broker:29092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            services.AddSingleton<IConsumer<Null, string>>(x => new ConsumerBuilder<Null, string>(config).Build());
            services.AddSingleton<IConsumerApplication, ConsumerApplication>();

            services.AddHostedService<UsuarioActualizadoHostedService>();
            services.AddHostedService<UsuarioRegistradoHostedService>();
            services.AddHostedService<UsuarioEliminadoHostedService>();

            services.AddHostedService<BannerPrincipalActualizadoHostedService>();
            services.AddHostedService<BannerPrincipalRegistradoHostedService>();
            services.AddHostedService<BannerPrincipalEliminadoHostedService>();

            services.AddHostedService<BoletinActualizadoHostedService>();
            services.AddHostedService<BoletinRegistradoHostedService>();
            services.AddHostedService<BoletinEliminadoHostedService>();

            services.AddHostedService<EmpresaActualizadoHostedService>();
            services.AddHostedService<EmpresaRegistradoHostedService>();
            services.AddHostedService<EmpresaEliminadoHostedService>();

            services.AddHostedService<ParametroActualizadoHostedService>();
            services.AddHostedService<ParametroRegistradoHostedService>();
            services.AddHostedService<ParametroEliminadoHostedService>();

            services.AddHostedService<ServicioBeneficioActualizadoHostedService>();
            services.AddHostedService<ServicioBeneficioRegistradoHostedService>();
            services.AddHostedService<ServicioBeneficioEliminadoHostedService>();

            return services;
        }
    }
}