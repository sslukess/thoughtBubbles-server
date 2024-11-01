using Microsoft.AspNetCore.Mvc;

namespace thoughtBubbles_server.Controllers;

[ApiController]
[Route("[controller]")]
public class ThoughtBubblesController : ControllerBase
{
    private readonly ILogger<ThoughtBubblesController> _logger; 

    public ThoughtBubblesController(ILogger<ThoughtBubblesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string Get()
    {
        return "a"; 
    }
}