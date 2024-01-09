using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace API_Service;

public static class Utils
{
    public static IChannel GetChannel(string name)
    {
        var channel = GetChannel();

        channel.QueueDeclare(queue: name,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        return channel;
    }

    static IChannel GetChannel()
    {
        var factory = new ConnectionFactory { HostName = "172.17.0.1"};
        IConnection connection = null; 
        IChannel channel = null;
        try
        {
            connection = factory.CreateConnection();
            channel = connection.CreateChannel();
        }
        catch (BrokerUnreachableException ex)
        {
            if ((DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).Seconds > 60)
            {
                return null;
            }
            Thread.Sleep(5000);
            return GetChannel();
        }

        return channel;
    }
}