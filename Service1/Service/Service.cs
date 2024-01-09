using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace API_Service;

public class Service
{
    IChannel _requestChannel;
    IChannel _responseChannel;

    List<string> _storedResults = new();

    public List<string> StoredResults => _storedResults;

    public Service(IChannel requestChannel, IChannel responseChannel)
    {
        _requestChannel = requestChannel;
        _responseChannel = responseChannel;
        
        var consumer = new EventingBasicConsumer(_requestChannel);
        consumer.Received += (model, ea) =>
        {
            _storedResults.Add(Encoding.UTF8.GetString(ea.Body.ToArray()));
        };
        _requestChannel.BasicConsume(queue: "response",
            autoAck: true,
            consumer: consumer);
    }

    public void Serve(string request)
    {
        var body = Encoding.UTF8.GetBytes(request);
        _requestChannel.BasicPublish(new CachedString(""), new CachedString("request"), new BasicProperties(), body);
    }
}   