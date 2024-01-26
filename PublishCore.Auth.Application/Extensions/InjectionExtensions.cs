using Confluent.Kafka;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublishCore.Auth.Application.Extensions.WatchDog;
using PublishCore.Auth.Application.Interfaces;
using PublishCore.Auth.Application.Services;
using System.Reflection;

namespace PublishCore.Auth.Application.Extensions
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

            services.AddScoped<IAuthApplication, AuthApplication>();
            services.AddScoped<IUsuarioApplication, UsuarioApplication>();
            services.AddScoped<IEmpresaApplication, EmpresaApplication>();

            var config = new ProducerConfig { BootstrapServers = "localhost:9092,190.113.124.155:9092" };

            services.AddSingleton<IProducer<Null, string>>(x => new ProducerBuilder<Null, string>(config).Build());
            services.AddSingleton<IProducerApplication, ProducerApplication>();

            services.AddWatchDog();

            return services;
        }
    }
}