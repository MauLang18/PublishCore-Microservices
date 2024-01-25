using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using PublishCore.Hubs.Application.Hubs;
using PublishCore.Hubs.Application.Interfaces;

namespace PublishCore.Hubs.Application.Services
{
    public class ConsumerApplication : IConsumerApplication
    {
        private readonly IHubContext<PublishCoreHubs> _hubContext;
        private readonly IConsumer<Null, string> _consumer;

        public ConsumerApplication(IHubContext<PublishCoreHubs> hubContext, IConsumer<Null, string> consumer)
        {
            _hubContext = hubContext;
            _consumer = consumer;
        }

        public void StartConsuming(string topic, string topicSignalR)
        {
            _consumer.Subscribe(topic);

            CancellationTokenSource token = new();

            try
            {
                while (true)
                {
                    var consumeResult = _consumer.Consume(token.Token);

                    if (consumeResult != null && consumeResult.Message != null)
                    {
                        var message = consumeResult.Message.Value;

                        Console.WriteLine(message);

                        _hubContext.Clients.All.SendAsync(topicSignalR, message);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}