using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using PublishCore.Hubs.Application.Hubs;
using PublishCore.Hubs.Application.Interfaces;

namespace PublishCore.Hubs.Application.HostedServices
{
    public class PublishCoreHostedServices : IHostedService, IDisposable
    {
        private readonly IConsumerApplication _consumerApplication;
        private readonly IHubContext<PublishCoreHubs> _hubContext;
        private Timer _timer;

        public PublishCoreHostedServices(IConsumerApplication consumerApplication, IHubContext<PublishCoreHubs> hubContext)
        {
            _consumerApplication = consumerApplication;
            _hubContext = hubContext;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(async state => await SendInfo(state!), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            await Task.CompletedTask;
        }

        private async Task SendInfo(object state)
        {
            await _consumerApplication.StartConsuming("PublishCore", "PublishCore");
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