using ChatManager.WebApi.Interfaces;
using Confluent.Kafka;

namespace ChatManager.WebApi.Services
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly ProducerConfig _config;
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(ProducerConfig config, IProducer<Null, string> producer)
        {
            _config = config;
            _producer = producer;
        }

        public async Task ProduceMessage(string topic, string message)
        {
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}
