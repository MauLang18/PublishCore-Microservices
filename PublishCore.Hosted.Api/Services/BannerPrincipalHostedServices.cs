using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PublishCore.Hosted.Api.Entities;
using PublishCore.Hosted.Api.Interfaces;
using PublishCore.Hosted.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublishCore.Hosted.Api.Services
{
    public class BannerPrincipalHostedServices : IHostedService, IDisposable
    {
        private readonly IProducerApplication _producer;
        private Timer _timer;

        public BannerPrincipalHostedServices(IProducerApplication producer)
        {
            _producer = producer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendInfo, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private async void SendInfo(object state)
        {
            IEnumerable<TbBannerPrincipal> bannerPrincipals;

            using (var context = new DB_TRANQUIContext())
            {
                bannerPrincipals = context.TbBannerPrincipals.ToList();
            }

            if (bannerPrincipals != null)
            {
                foreach (var item in bannerPrincipals)
                {
                    int id = item.Id;
                    int empresaId = item.EmpresaId;
                    string nombre = item.Nombre ?? string.Empty;
                    string imagen = item.Imagen ?? string.Empty;
                    DateTime fechaCreacion = item.FechaCreacionAuditoria;
                    DateTime? fechaEliminacion = item.FechaEliminacionAuditoria;
                    int? usuarioEliminacion = item.UsuarioEliminacionAuditoria;
                    int estado = item.Estado;
                    int? programacion = item.Programacion;
                    DateTime? fechaProgramacion = item.FechaProgramacion;

                    if (fechaEliminacion == null && usuarioEliminacion == null)
                    {
                        if (programacion == (int)StateTypes.Activo &&
                        fechaProgramacion <= DateTime.Now &&
                        estado == (int)StateTypes.Inactivo)
                        {
                            Console.WriteLine($"ID: {id}");
                            Console.WriteLine($"Nombre: {nombre}");
                            Console.WriteLine("Programación activa y fecha de programación coincide con la actual.");
                            Console.WriteLine("-----------");

                            item.Id = id;
                            item.Nombre = nombre;
                            item.Imagen = imagen;
                            item.EmpresaId = empresaId;
                            item.FechaActualizacionAuditoria = DateTime.Now;
                            item.UsuarioActualizacionAuditoria = 1;
                            item.Programacion = (int)StateTypes.Inactivo;
                            item.Estado = (int)StateTypes.Activo;

                            using (var updateContext = new DB_TRANQUIContext())
                            {
                                updateContext.TbBannerPrincipals.Update(item);
                                await updateContext.SaveChangesAsync();
                            }

                            await _producer.ProduceAsync("bannerActualizado", JsonConvert.SerializeObject(item));
                        }
                        else
                        {
                            Console.WriteLine($"La programación no está activa o la fecha de programación no coincide con la actual para el ID: {id}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No se pudieron obtener los datos de los banners de la base de datos.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}