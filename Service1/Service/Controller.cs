using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API_Service;

[ApiController]
[Route("/")]
public class Controller: ControllerBase
{
    Service _service;

    public Controller(Service service)
    {
        _service = service;
    }

    
    
    [HttpPost("/")]
    public IActionResult Post([FromBody] Request request)
    {
        _service.Serve(request.Body);
        return Ok();
    }

    [HttpGet("/")]
    public List<string> Get()
    {
        return _service.StoredResults;
    }
}

public class Request
{
    public string Body { get; set; }
}