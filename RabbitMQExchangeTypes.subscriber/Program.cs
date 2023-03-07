using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://hmdtzelt:1GHaeNWNZBVBux7YQFkpa_74fee5eLwY@hawk.rmq.cloudamqp.com/hmdtzelt");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();
var queueName = "hello-queue";
var consumer = new EventingBasicConsumer(channel);

channel.BasicQos(0,2,false);
channel.BasicConsume(queue: queueName, false, consumer);

consumer.Received += (model, ea) =>
{
    Thread.Sleep(500);
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Mesaj alındı: {message}");
    channel.BasicAck(ea.DeliveryTag, false);
};


Console.ReadLine();