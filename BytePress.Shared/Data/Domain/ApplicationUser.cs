using Microsoft.AspNetCore.Identity;

namespace BytePress.Shared.Data.Domain;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}
