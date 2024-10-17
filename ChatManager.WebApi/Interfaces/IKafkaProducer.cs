namespace ChatManager.WebApi.Interfaces
{
    public interface IKafkaProducer
    {
        Task ProduceMessage(string topic, string message);
    }
}
