using API_Service;
var requestChannel = Utils.GetChannel("request");
var responseChannel = Utils.GetChannel("response");
var service2 = new Service(requestChannel, responseChannel);
Thread.Sleep(Timeout.Infinite);