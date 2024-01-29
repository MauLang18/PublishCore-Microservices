using PublishCore.Publish.Api.Extensions;
using PublishCore.Publish.Application.Extensions;
using PublishCore.Publish.Infrastructure.Extensions;
using PublishCore.Auth.JWT;
using WatchDog;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.
var Cors = "Cors";

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = null; // O establece el tama�o deseado aqu�
});

builder.Services.AddInjectionInfrastructure(Configuration);
builder.Services.AddInjectionApplication(Configuration);
builder.Services.AddAuthentication(Configuration);
builder.Services.AddSwagger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
        builder =>
        {
            builder.WithOrigins("http://localhost:5173", "https://tranquiexpress.com", "https://www.tranquiexpress.com", "http://localhost:4200", "https://motornovacr.com", "https://www.motornovacr.com", "http://190.113.124.155:9091");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
            builder.AllowCredentials();
        });
});

builder.Services.Configure<MvcOptions>(options =>
{
    options.MaxModelBindingCollectionSize = int.MaxValue;
});

var app = builder.Build();

app.UseCors(Cors);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWatchDogExceptionLogger();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseWatchDog(configuration =>
{
    configuration.WatchPageUsername = "admin";
    configuration.WatchPagePassword = "admin";
});

app.Run();