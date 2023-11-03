using Confluent.Kafka;


namespace Server.Kafka
{
    public class Consumer<T> : IDisposable, IConsumer<T> where T : class, new()
    {
        private bool _disposed = false;

        private IConsumer<Ignore, byte[]> _kafkaConsumer;
        public string Topic { get; }

        public event KafkaReceivedEvent<T>? _receiveEvent;


        public Consumer(string topic)
        {
            Topic = topic;
            var boostrapServerKafkaIP = Environment.GetEnvironmentVariable("KafkaBootstrapServerIP");
            var boostrapServerKafkaPort = Environment.GetEnvironmentVariable("KafkaBootstrapServerPort");
            Console.WriteLine(boostrapServerKafkaIP);

            var config = new ConsumerConfig
            {
                GroupId = Environment.GetEnvironmentVariable("KafkaBootstrapServerGroupId"),
                BootstrapServers = $"{boostrapServerKafkaIP}:{boostrapServerKafkaPort}",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                AllowAutoCreateTopics = true
            };

            _kafkaConsumer = new ConsumerBuilder<Ignore, byte[]>(config).Build();
            try
            {
                _kafkaConsumer.Subscribe(Topic);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
          

            CancellationTokenSource cancellationToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                ReceiveMessage(cancellationToken.Token);
            });
        }

        private void ReceiveMessage(CancellationToken cancellationToken)
        {
            try
            {

                while (!_disposed)
                {
                    var consumer = _kafkaConsumer.Consume(cancellationToken);
                    byte[] message = consumer.Message.Value;

                    if (message.Length != 0)
                    {
                        T received = new T();
                        try
                        {
                            _receiveEvent(KafkaMessage<T>.TCDeserialize(message));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _kafkaConsumer.Close();
            }
        }


        public void Dispose()
        {
            _disposed = true;
            _kafkaConsumer.Dispose();
        }
    }
}
