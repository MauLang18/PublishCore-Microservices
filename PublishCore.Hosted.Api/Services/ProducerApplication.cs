using Confluent.Kafka;
using PublishCore.Hosted.Api.Interfaces;
using System.Threading.Tasks;

namespace PublishCore.Hosted.Api.Services
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