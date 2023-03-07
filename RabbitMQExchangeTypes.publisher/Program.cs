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

        var exhangeName = "hello-direct-exchange";
        channel.ExchangeDeclare(exhangeName, ExchangeType.Direct, true, false, null);

        Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
        {
            var routeKey = $"route-{x}";
            var queueName = $"direct-queue-{x}";
            channel.QueueDeclare(queueName, true, false, false);

            channel.QueueBind(queueName, exhangeName,routeKey,null);

        });

        Enumerable.Range(0, 100).ToList().ForEach(i =>
        {
            LogNames logName = (LogNames) new Random().Next(1, 5);
            var message = $"{logName} - {i}";
            var messageBody = Encoding.UTF8.GetBytes(message);
            var routeKey = $"route-{logName}";
            channel.BasicPublish(exhangeName, routeKey, null, messageBody);
            Console.WriteLine($"Log gönderilmiştir : {message}");
        });

        Console.WriteLine("Mesaj gönderilmiştir...");

        Console.ReadLine();
    }
}

