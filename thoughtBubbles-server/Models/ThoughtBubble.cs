using System.ComponentModel.DataAnnotations; 

namespace ThoughtBubbles.Models; 

public class ThoughtBubble
{
    public int Id { get; set; }
    public string thought { get; set; } = "";
    public string[]? topics = [];  
}