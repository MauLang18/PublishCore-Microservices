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
    public class ServicioBeneficioHostedServices : IHostedService, IDisposable
    {
        private readonly IProducerApplication _producer;
        private Timer _timer;

        public ServicioBeneficioHostedServices(IProducerApplication producer)
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
            IEnumerable<TbServicioBeneficio> bannerPrincipals;

            using (var context = new DB_TRANQUIContext())
            {
                bannerPrincipals = context.TbServicioBeneficios.ToList();
            }

            if (bannerPrincipals != null)
            {
                foreach (var item in bannerPrincipals)
                {
                    int id = item.Id;
                    int empresaId = item.EmpresaId;
                    string titulo = item.Titulo ?? string.Empty;
                    string descripcion = item.Descripcion ?? string.Empty;
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
                            item.Id = id;
                            item.EmpresaId = empresaId;
                            item.Titulo = titulo;
                            item.Descripcion = descripcion;
                            item.Imagen = imagen;
                            item.FechaActualizacionAuditoria = DateTime.Now;
                            item.UsuarioActualizacionAuditoria = 1;
                            item.Programacion = (int)StateTypes.Inactivo;
                            item.Estado = (int)StateTypes.Activo;

                            using (var updateContext = new DB_TRANQUIContext())
                            {
                                updateContext.TbServicioBeneficios.Update(item);
                                await updateContext.SaveChangesAsync();
                            }

                            await _producer.ProduceAsync("servicioBeneficioActualizado", JsonConvert.SerializeObject(item));
                        }
                    }
                }
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