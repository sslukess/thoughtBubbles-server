using Microsoft.AspNetCore.Mvc;
using ThoughtBubbles.Services;
using ThoughtBubbles.Models;

namespace thoughtBubbles_server.Controllers;

[ApiController]
[Route("[controller]")]
public class ThoughtBubblesController : ControllerBase
{
    private readonly ILogger<ThoughtBubblesController> _logger;
    ThoughtBubblesService _service;

    public ThoughtBubblesController(
        ILogger<ThoughtBubblesController> logger, 
        ThoughtBubblesService service)
    {
        _logger = logger;
        _service = service;

    }

    [HttpGet]
    public IEnumerable<ThoughtBubble> Get()
    {
        return _service.GetAllBubbles();
    }

    [HttpGet("{id}")]
    public ActionResult<ThoughtBubble> GetById(int id)
    {
        var thoughtBubble = _service.GetById(id);

        if(thoughtBubble is not null)
        {
            return thoughtBubble;
        }
        else
        {
            return NotFound();
        }
    }


    [HttpPost]
    public IActionResult Create(NoIdThoughtBubble newThoughtBubble)
    {
        var thoughtBubble = _service.Create(newThoughtBubble);
        return CreatedAtAction(nameof(GetById), new { id = thoughtBubble!.Id }, thoughtBubble);
    }

    [HttpPost("delete/{id}")]
    public IActionResult Delete(int id)
    {
        ThoughtBubble? bubbleToDelete = _service.GetById(id);

        if(bubbleToDelete is not null)
        {
            _service.DeleteById(id); 
            return Ok(); 
        }
        else
        {
            return NotFound();
        }
    }
}