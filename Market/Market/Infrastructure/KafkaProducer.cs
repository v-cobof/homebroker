using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

public class KafkaProducer
{
    private readonly ILogger<KafkaProducer> _logger;
    private readonly ProducerConfig _producerConfig;

    public KafkaProducer(ILogger<KafkaProducer> logger)
    {
        _logger = logger;

        // Configuração do Kafka Producer
        _producerConfig = new ProducerConfig
        {
            BootstrapServers = "localhost:9092", // Servidor Kafka
            Acks = Acks.All, // Confirmação de todos os brokers (maior confiabilidade)
            EnableIdempotence = true, // Garante que mensagens não serão duplicadas
        };
    }

    public async Task SendMessageAsync(string topic, string key, string value)
    {
        using var producer = new ProducerBuilder<string, string>(_producerConfig).Build();

        try
        {
            var message = new Message<string, string>
            {
                Key = key,
                Value = value
            };

            // Envia a mensagem para o tópico especificado
            var deliveryResult = await producer.ProduceAsync(topic, message);

            _logger.LogInformation($"Mensagem enviada para o tópico {topic} com chave {key}: {value}. Offset: {deliveryResult.Offset}");
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError($"Erro ao enviar mensagem: {ex.Error.Reason}");
        }
    }
}