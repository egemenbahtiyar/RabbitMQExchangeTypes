using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://hmdtzelt:1GHaeNWNZBVBux7YQFkpa_74fee5eLwY@hawk.rmq.cloudamqp.com/hmdtzelt");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();
         

channel.BasicQos(0, 1, false);
var consumer = new EventingBasicConsumer(channel);

var queueName = channel.QueueDeclare().QueueName;
var routekey = "*.Error.*";
var exhangeName = "hello-topic-exchange";
channel.QueueBind(queueName, exhangeName, routekey);

channel.BasicConsume(queueName,false, consumer);

Console.WriteLine("Logları dinleniyor...");

consumer.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Thread.Sleep(1500);
    Console.WriteLine("Gelen Mesaj:" + message);

    // File.AppendAllText("log-critical.txt", message+ "\n");

    channel.BasicAck(e.DeliveryTag, false);
};

Console.ReadLine();