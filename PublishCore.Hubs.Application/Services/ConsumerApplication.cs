using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using PublishCore.Hubs.Application.Hubs;
using PublishCore.Hubs.Application.Interfaces;

namespace PublishCore.Hubs.Application.Services
{
    public class ConsumerApplication : IConsumerApplication
    {
        private readonly IConsumer<Null, string> _consumer;
        private readonly IHubContext<PublishCoreHubs> _hubContext;

        public ConsumerApplication(IConsumer<Null, string> consumer, IHubContext<PublishCoreHubs> hubContext)
        {
            _consumer = consumer;
            _hubContext = hubContext;
        }

        public async Task<string> StartConsuming(string topic, string signalR)
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

                        Console.WriteLine(signalR);
                        Console.WriteLine($"topic {topic}");
                        Console.WriteLine($"topic primera mayuscula {char.ToUpper(topic[0]) + topic.Substring(1)}");
                        Console.WriteLine("consumer: " + message);
                        await _hubContext.Clients.All.SendAsync(signalR, message);

                        return message;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _consumer.Unsubscribe();
            }

            return null!;
        }
    }
}