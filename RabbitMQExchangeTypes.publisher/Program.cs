
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://hmdtzelt:1GHaeNWNZBVBux7YQFkpa_74fee5eLwY@hawk.rmq.cloudamqp.com/hmdtzelt");

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();

var queueName = "hello-queue";
channel.QueueDeclare(queueName, true, false, false);

string message = "hello world";

var messageBody = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

Console.WriteLine("Mesaj gönderilmiştir");

Console.ReadLine();

