using System.Text;
using RabbitMQ.Client;

public enum LogNames
{
    Critical = 1,
    Error = 2,
    Warning = 3,
    Info = 4
}

internal class Program
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory();
        factory.Uri = new Uri("amqps://hmdtzelt:1GHaeNWNZBVBux7YQFkpa_74fee5eLwY@hawk.rmq.cloudamqp.com/hmdtzelt");

        using var connection = factory.CreateConnection();

        var channel = connection.CreateModel();
        var exhangeName = "hello-header-exchange";

        channel.ExchangeDeclare(exhangeName, durable: true, type: ExchangeType.Headers);
        Dictionary<string, object> headers = new Dictionary<string, object>();

        headers.Add("format", "pdf");
        headers.Add("shape", "a4");

        var properties = channel.CreateBasicProperties();
        properties.Headers = headers;
        

        channel.BasicPublish(exhangeName,string.Empty, properties, Encoding.UTF8.GetBytes("Header message"));

        Console.WriteLine($"Mesaj gönderilmiştir.");



        Console.ReadLine();
    }
}

