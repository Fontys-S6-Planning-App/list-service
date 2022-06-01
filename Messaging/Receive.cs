using RabbitMQ.Client.Events;

namespace list_service.Messaging;

using RabbitMQ.Client;
using System.Text;
using System;

public static class Receive
{
    public static void ReceiveMessage()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "board",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "board",
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}