using System.Diagnostics;
using System.Net;
using Confluent.Kafka;

namespace OrderManagement.Services;

public class ProducerService
{
    private readonly string _bootstrapServers = "localhost:9092";
    private readonly string _topic = "orders";

    public async Task<bool> SendOrderRequest(string message)
    {
        var config = new ProducerConfig {
            BootstrapServers = _bootstrapServers,
            ClientId = Dns.GetHostName()
        };

        try
        {
            using var producer = new ProducerBuilder
                <Null, string> (config).Build();
            var result = await producer.ProduceAsync
            (_topic, new Message <Null, string> {
                Value = message
            });
            
            return await Task.FromResult(true);
        } catch (Exception ex) {
            Console.WriteLine($"Error occured: {ex.Message}");
        }

        return await Task.FromResult(false);
    }
}