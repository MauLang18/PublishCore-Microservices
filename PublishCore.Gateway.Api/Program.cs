using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);
var Cors = "Cors";

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

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

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue; // o el tama�o m�ximo deseado
});

var app = builder.Build();

app.UseCors(Cors);

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.UseWebSockets();
app.UseOcelot().Wait();

app.Run();