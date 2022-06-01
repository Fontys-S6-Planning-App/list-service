using Microsoft.EntityFrameworkCore.Infrastructure;

namespace list_service.Messaging;
using RabbitMQ.Client;
using System.Text;
using System;

public class Send
{
    private IConnection _connection;
    
    public Send()
    {
        CreateConnection();
    }
    
    private void CreateConnection()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = "38.242.252.134",
            };
            _connection = factory.CreateConnection();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void SendMessage(string message)
    {
        using (var channel = _connection.CreateModel())
        {
            channel.QueueDeclare(queue: "list_delete",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            string messageBody = message;
            var body = Encoding.UTF8.GetBytes(messageBody);
            channel.BasicPublish(exchange: "",
                                 routingKey: "list_delete",
                                 basicProperties: null,
                                 body: body);
        }
    }
}