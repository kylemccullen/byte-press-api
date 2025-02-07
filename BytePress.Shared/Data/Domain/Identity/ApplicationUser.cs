using Microsoft.AspNetCore.Identity;

namespace BytePress.Shared.Data.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }

    public List<ApplicationUserRole> UserRoles { get; set; }

    public List<ApplicationUserClaim> UserClaims { get; set; }

    public List<Task> Tasks { get; set; }
}
