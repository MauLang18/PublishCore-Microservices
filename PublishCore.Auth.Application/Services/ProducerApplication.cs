using Confluent.Kafka;
using PublishCore.Auth.Application.Interfaces;

namespace PublishCore.Auth.Application.Services
{
    public class ProducerApplication : IProducerApplication
    {
        private readonly IProducer<Null, string> _producer;

        public ProducerApplication(IProducer<Null, string> producer)
        {
            _producer = producer;
        }

        public async Task ProduceAsync(string topic, string message)
        {
            await _producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = message
            });
        }
    }
}