using Microsoft.EntityFrameworkCore;
using ThoughtBubbles.Data;
using ThoughtBubbles.Models; 

namespace ThoughtBubbles.Services;
public class ThoughtBubblesService
{

    private readonly ThoughtBubblesContext _context;
    public ThoughtBubblesService(ThoughtBubblesContext context)
    {
        _context = context; 
    }

    public IEnumerable<ThoughtBubble> GetAllBubbles()
    {
        return _context.ThoughtBubbles.AsNoTracking().ToList(); 
    }

    public ThoughtBubble? GetById(int id)
    {
        ThoughtBubble? fetchedThoughtBubble = _context.ThoughtBubbles
                                .AsNoTracking()
                                .SingleOrDefault(p => p.Id == id);

        return fetchedThoughtBubble;
    }

    public ThoughtBubble? Create(ThoughtBubble newThoughtBubble)
    {
        _context.Add(newThoughtBubble);
        _context.SaveChanges();

        // Returning the bubble from the db, not just returning the argument newPizza
        return GetById(newThoughtBubble.Id);
    }
}