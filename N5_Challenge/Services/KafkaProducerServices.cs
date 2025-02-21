using Confluent.Kafka;
using N5_Challenge.Configuration.Options;
using N5_Challenge.Exeptions;
using N5_Challenge.Services.Interface;
using static N5_Challenge.Helper.Enums;

namespace N5_Challenge.Services
{
    public class KafkaProducerServices : IKafkaProducerServices
    {
        private readonly KafkaOptions _kafkaOptions;

        public KafkaProducerServices(KafkaOptions kafkaOptions)
        {
            _kafkaOptions = kafkaOptions;
        }

        public async Task EnviarEventoAsync(TipoOperacionesKafka tipoOperacion)
        {
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = _kafkaOptions.Server
                };

                using (var producer = new ProducerBuilder<string, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(_kafkaOptions.Topic, new Message<string, string>
                    {
                        Key = Guid.NewGuid().ToString(),
                        Value = tipoOperacion.ToString()
                    });

                    Console.WriteLine($"Mensaje enviado a Kafka: {result.TopicPartitionOffset}");
                }
            }
            catch (UncontroledException e) { throw e; }
        }
    }
}
