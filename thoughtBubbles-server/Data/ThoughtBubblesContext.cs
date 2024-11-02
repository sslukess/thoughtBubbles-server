using Microsoft.EntityFrameworkCore;
using ThoughtBubbles.Models;

namespace ThoughtBubbles.Data; 

public class ThoughtBubblesContext : DbContext
{
    public ThoughtBubblesContext (DbContextOptions<ThoughtBubblesContext> options)
        : base(options)
    {   

    }

    public DbSet<ThoughtBubble> ThoughtBubbles => Set<ThoughtBubble>(); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ThoughtBubble>()
            .HasKey(tb => tb.Id); // set ID as the key
    }
}