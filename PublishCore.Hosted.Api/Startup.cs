using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PublishCore.Hosted.Api.Interfaces;
using PublishCore.Hosted.Api.Services;

namespace PublishCore.Hosted.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:5173", "https://tranquiexpress.com", "https://www.tranquiexpress.com", "http://localhost:4200", "https://motornovacr.com", "https://www.motornovacr.com", "http://190.113.124.155:9091")
                        .AllowCredentials());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PublishCoreHosted", Version = "v1" });
            });

            var config = new ProducerConfig { BootstrapServers = "broker:29092" };

            services.AddSingleton<IProducer<Null, string>>(x => new ProducerBuilder<Null, string>(config).Build());
            services.AddSingleton<IProducerApplication, ProducerApplication>();

            services.AddHostedService<BannerPrincipalHostedServices>();
            services.AddHostedService<BoletinHostedServices>();
            services.AddHostedService<ServicioBeneficioHostedServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PublishCoreHosted v1"));
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}