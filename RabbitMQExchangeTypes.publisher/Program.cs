using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://hmdtzelt:1GHaeNWNZBVBux7YQFkpa_74fee5eLwY@hawk.rmq.cloudamqp.com/hmdtzelt");

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();

var exhangeName = "hello-fanout-exchange";
channel.ExchangeDeclare(exhangeName, ExchangeType.Fanout, true, false, null);

Enumerable.Range(0, 100).ToList().ForEach(i =>
{
    var message = $"hello world {i}";
    var messageBody = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exhangeName, string.Empty, null, messageBody);
});

Console.WriteLine("Mesaj gönderilmiştir");

Console.ReadLine();

