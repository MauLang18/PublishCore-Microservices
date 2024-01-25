using Microsoft.Extensions.Hosting;
using PublishCore.Hubs.Application.Interfaces;

namespace PublishCore.Hubs.Application.HostedServices
{
    public class EmpresaHostedService : IHostedService, IDisposable
    {
        private readonly IConsumerApplication _consumerApplication;
        private Timer _timer;

        public EmpresaHostedService(IConsumerApplication consumerApplication)
        {
            _consumerApplication = consumerApplication;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(SendInfo, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private async void SendInfo(object state)
        {
            _consumerApplication.StartConsuming("empresaActualizado", "EmpresaActualizado");
            Console.WriteLine("1");

            _consumerApplication.StartConsuming("empresaEliminado", "EmpresaEliminado");
            Console.WriteLine("2");

            _consumerApplication.StartConsuming("empresaRegistrado", "EmpresaRegistrado");
            Console.WriteLine("3");
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