namespace PublishCore.Hubs.Application.Interfaces
{
    public interface IConsumerApplication
    {
        public Task<string> StartConsuming(string topic, string topicSignalR);
    }
}