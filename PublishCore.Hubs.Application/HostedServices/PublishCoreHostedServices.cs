using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using PublishCore.Hubs.Application.Hubs;
using PublishCore.Hubs.Application.Interfaces;

namespace PublishCore.Hubs.Application.HostedServices
{
    public class PublishCoreHostedServices :IHostedService, IDisposable
    {
        private readonly IConsumerApplication _consumerApplication;
        private readonly IHubContext<PublishCoreHubs> _hubContext;
        private Timer _timer;
        private int i = 0;
        private string topic = "";
        private string signalR = "";

        public PublishCoreHostedServices(IConsumerApplication consumerApplication, IHubContext<PublishCoreHubs> hubContext)
        {
            _consumerApplication = consumerApplication;
            _hubContext = hubContext;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(async state => await SendInfo(state!), null, TimeSpan.Zero, TimeSpan.FromMilliseconds(55.56));
            await Task.CompletedTask;
        }

        private async Task SendInfo(object state)
        {
            int originalI = i;

            switch (originalI)
            {
                case 0:
                    topic = "bannerRegistrado";
                    signalR = "BannerRegistrado";
                    i++;
                    break;
                case 1:
                    topic = "bannerActualizado";
                    signalR = "BannerActualizado";
                    i++;
                    break;
                case 2:
                    topic = "bannerEliminado";
                    signalR = "BannerEliminado";
                    i++;
                    break;
                case 3:
                    topic = "boletinRegistrado";
                    signalR = "BoletinRegistrado";
                    i++;
                    break;
                case 4:
                    topic = "boletinActualizado";
                    signalR = "BoletinActualizado";
                    i++;
                    break;
                case 5:
                    topic = "boletinEliminado";
                    signalR = "BoletinEliminado";
                    i++;
                    break;
                case 6:
                    topic = "empresaRegistrado";
                    signalR = "EmpresaRegistrado";
                    i++;
                    break;
                case 7:
                    topic = "empresaActualizado";
                    signalR = "EmpresaActualizado";
                    i++;
                    break;
                case 8:
                    topic = "empresaEliminado";
                    signalR = "EmpresaEliminado";
                    i++;
                    break;
                case 9:
                    topic = "parametroRegistrado";
                    signalR = "ParametroRegistrado";
                    i++;
                    break;
                case 10:
                    topic = "parametroActualizado";
                    signalR = "ParametroActualizado";
                    i++;
                    break;
                case 11:
                    topic = "parametroEliminado";
                    signalR = "ParametroEliminado";
                    i++;
                    break;
                case 12:
                    topic = "servicioBeneficioRegistrado";
                    signalR = "ServicioBeneficioRegistrado";
                    i++;
                    break;
                case 13:
                    topic = "servicioBeneficioActualizado";
                    signalR = "ServicioBeneficioActualizado";
                    i++;
                    break;
                case 14:
                    topic = "servicioBeneficioEliminado";
                    signalR = "ServicioBeneficioEliminado";
                    i++;
                    break;
                case 15:
                    topic = "usuarioRegistrado";
                    signalR = "UsuarioRegistrado";
                    i++;
                    break;
                case 16:
                    topic = "usuarioActualizado";
                    signalR = "UsuarioActualizado";
                    i++;
                    break;
                case 17:
                    topic = "usuarioEliminado";
                    signalR = "UsuarioEliminado";
                    i++;
                    break;
            }

            if (originalI == 18)
            {
                i = 0;
            }

            var message = await _consumerApplication.StartConsuming(topic, signalR);
            
            if (message != null)
            {
                Console.WriteLine(signalR);
                Console.WriteLine($"topic {topic}");
                Console.WriteLine("signalR: " + message);
                await _hubContext.Clients.All.SendAsync("ParametroActualizado", message);
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