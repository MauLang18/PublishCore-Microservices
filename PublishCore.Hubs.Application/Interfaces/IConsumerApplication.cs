namespace PublishCore.Hubs.Application.Interfaces
{
    public interface IConsumerApplication
    {
        public void StartConsuming(string topic, string topicSignalR);
    }
}