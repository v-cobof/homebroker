using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Market.Application.DTO;
using Market.Application.Mappers;
using Market.Domain.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Market.Infrastructure
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly ILogger<KafkaConsumer> _logger;
        private readonly string _topic;
        private readonly ConsumerConfig _consumerConfig;
        private BookService _bookService;
        private KafkaProducer _producer;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, KafkaProducer producer)
        {
            _logger = logger;

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092", // Servidor Kafka
                GroupId = "example-consumer-group", // Grupo do consumidor
                AutoOffsetReset = AutoOffsetReset.Latest, // Lê mensagens desde o início se não houver commit
                EnableAutoCommit = true, // Commit automático das mensagens
                AllowAutoCreateTopics = true // Permite criar o tópico automaticamente
            };

            _topic = "input"; // Nome do tópico Kafka
            _bookService = new BookService();
            _producer = producer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = Task.Run(() => _bookService.Trade(stoppingToken), stoppingToken);
            _ = Task.Run(() => PublishOutput(), stoppingToken);

            _logger.LogInformation("Kafka consumer iniciado.");
            using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();

            consumer.Subscribe(_topic);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(stoppingToken);

                        _logger.LogInformation($"Mensagem recebida: {consumeResult.Message.Value}");

                        var tradeInput = JsonSerializer.Deserialize<TradeInput>(consumeResult.Message.Value);
                        var order = Mapper.MapOrderFromInput(tradeInput);

                        _bookService.AddOrder(order);
                    }
                    catch (ConsumeException ex)
                    {
                        _logger.LogError($"Erro ao consumir mensagem: {ex.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Consumo Kafka cancelado.");
            }
            finally
            {
                // Fecha o consumidor
                consumer.Close();
                _logger.LogInformation("Kafka consumer finalizado.");
            }
        }

        private async Task PublishOutput()
        {
            _logger.LogInformation("Publicando outputs.");

            foreach (var order in _bookService.GetOutputChannel().GetConsumingEnumerable())
            {
                var output = Mapper.MapOutputFromOrder(order);
                var json = JsonSerializer.Serialize(output);

                _logger.LogInformation($"Publicando output {output.OrderId}.");

                await _producer.SendMessageAsync("output", "orders", json);
            }
        }
    }
}
