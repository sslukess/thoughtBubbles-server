using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Database;

public class User : IdentityUser
{
    public string? Initials { get; set; } 
}