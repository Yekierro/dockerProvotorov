using System.Diagnostics;
using API_Service;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

var builder = WebApplication.CreateBuilder(args);

var requestChannel = Utils.GetChannel("request");
var responseChannel = Utils.GetChannel("response");


builder.Services.AddSingleton<Service>(_ => new Service(requestChannel, responseChannel));
builder.Services.AddSingleton<Controller>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
return 0;