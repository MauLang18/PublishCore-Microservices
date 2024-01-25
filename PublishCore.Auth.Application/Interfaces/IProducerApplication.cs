namespace PublishCore.Auth.Application.Interfaces
{
    public interface IProducerApplication
    {
        Task ProduceAsync(string topic, string message);
    }
}