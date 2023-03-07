using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://hmdtzelt:1GHaeNWNZBVBux7YQFkpa_74fee5eLwY@hawk.rmq.cloudamqp.com/hmdtzelt");

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();

var queueName = "hello-queue";
channel.QueueDeclare(queueName, true, false, false);

Enumerable.Range(0, 100).ToList().ForEach(i =>
{
    var message = $"hello world {i}";
    var messageBody = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(string.Empty, queueName, null, messageBody);
});

Console.WriteLine("Mesaj gönderilmiştir");

Console.ReadLine();

