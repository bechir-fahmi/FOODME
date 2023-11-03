using Confluent.Kafka;


namespace Server.Kafka
{
    public class Producer<T> : IProducer<T> where T : class, new()
    {
        private IProducer<Null, byte[]> _kafkaProducer;

        public string Topic { get; }

        public Producer(string topic)
        {
            Topic = topic;
            var boostrapServerKafkaIP = Environment.GetEnvironmentVariable("KafkaBootstrapServerIP");
            var boostrapServerKafkaPort = Environment.GetEnvironmentVariable("KafkaBootstrapServerPort");

            var config = new AdminClientConfig
            {
                BootstrapServers = $"{boostrapServerKafkaIP}:{boostrapServerKafkaPort}"
            };


            _kafkaProducer = new ProducerBuilder<Null, byte[]>(config).Build();
        }

        public async void sendMessage(T message, CancellationToken cancellationToken)
        {

            await _kafkaProducer.ProduceAsync(Topic, new Message<Null, byte[]>()
            {
                Value = KafkaMessage<T>.TCSerialize(message)
            }, cancellationToken);

            _kafkaProducer.Flush(TimeSpan.FromSeconds(10));
        }

        public void stop()
        {
            _kafkaProducer?.Dispose();
        }
    }
}
