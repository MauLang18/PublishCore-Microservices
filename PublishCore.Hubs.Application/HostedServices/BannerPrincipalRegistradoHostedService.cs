﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using PublishCore.Hubs.Application.Hubs;
using PublishCore.Hubs.Application.Interfaces;

namespace PublishCore.Hubs.Application.HostedServices
{
    public class BannerPrincipalRegistradoHostedService : IHostedService, IDisposable
    {
        private readonly IConsumerApplication _consumerApplication;
        private readonly IHubContext<PublishCoreHubs> _hubContext;
        private Timer _timer;

        public BannerPrincipalRegistradoHostedService(IConsumerApplication consumerApplication, IHubContext<PublishCoreHubs> hubContext)
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
            var message = _consumerApplication.StartConsuming("bannerRegistrado", "BannerRegistrado");

            if (message != null)
            {
                _hubContext.Clients.All.SendAsync("BannerRegistrado", message);
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