using System.ComponentModel.DataAnnotations;

namespace ThoughtBubbles.Models;

public class NoIdThoughtBubble
{
    public string Thought { get; set; } = "";
    public List<string>? Topics { get; set; } = new List<string>();
}
public class ThoughtBubble : NoIdThoughtBubble
{
    public int Id { get; set; }
}