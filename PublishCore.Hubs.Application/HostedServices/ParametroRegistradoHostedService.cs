﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using PublishCore.Hubs.Application.Hubs;
using PublishCore.Hubs.Application.Interfaces;

namespace PublishCore.Hubs.Application.HostedServices
{
    public class ParametroRegistradoHostedService : IHostedService, IDisposable
    {
        private readonly IConsumerApplication _consumerApplication;
        private readonly IHubContext<PublishCoreHubs> _hubContext;
        private Timer _timer;

        public ParametroRegistradoHostedService(IConsumerApplication consumerApplication, IHubContext<PublishCoreHubs> hubContext)
        {
            _consumerApplication = consumerApplication;
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(SendInfo, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void SendInfo(object state)
        {
            var message = _consumerApplication.StartConsuming("parametroRegistrado", "ParametroRegistrado");

            if (message != null)
            {
                _hubContext.Clients.All.SendAsync("ParametroRegistrado", message);
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