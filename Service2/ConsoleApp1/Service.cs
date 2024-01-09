using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace API_Service;

public class Service
{
    IChannel _requestChannel;
    IChannel _responseChannel;
    
    public Service(IChannel requestChannel, IChannel responseChannel)
    {
        _requestChannel = requestChannel;
        _responseChannel = responseChannel;
        
        var consumer = new EventingBasicConsumer(_requestChannel);
        consumer.Received += (model, ea) =>
        {
            Handle(ea.Body.ToArray());
        };
        _requestChannel.BasicConsume(queue: "request",
            autoAck: true,
            consumer: consumer);
    }

    void Handle(byte[] body)
    {
        var content = Encoding.UTF8.GetString(body);
        var result = InvertString(content);
        body = Encoding.UTF8.GetBytes(result);
            
        _responseChannel.BasicPublish(new CachedString(""), new CachedString("response"), new BasicProperties(), body);
    }
    
    string InvertString(string str)
    {
        var result = new StringBuilder();
        for (var i = str.Length - 1; i > -1; i--)
            result.Append(str[i]);

        return result.ToString();
    }
}