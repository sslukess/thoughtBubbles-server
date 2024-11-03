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

    public ThoughtBubble? Create(NoIdThoughtBubble newNoIdThoughtBubble)
    {
        var newThoughtBubble = new ThoughtBubble
        {
            Thought = newNoIdThoughtBubble.Thought,
            Topics = newNoIdThoughtBubble.Topics
            // The Id will be set automatically by the database
        };

        _context.ThoughtBubbles.Add(newThoughtBubble);
        _context.SaveChanges();

        // Returning the bubble from the db, not just returning the argument newThoughtBubble
        return GetById(newThoughtBubble.Id);
    }

    public void DeleteById(int id)
    {
        ThoughtBubble? bubbleToDelete = _context.ThoughtBubbles.Find(id);

        if (bubbleToDelete is not null)
        {
            _context.Remove(bubbleToDelete);
            _context.SaveChanges();
        }
    }
}