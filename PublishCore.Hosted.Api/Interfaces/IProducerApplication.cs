using System.Threading.Tasks;

namespace PublishCore.Hosted.Api.Interfaces
{
    public interface IProducerApplication
    {
        Task ProduceAsync(string topic, string message);
    }
}