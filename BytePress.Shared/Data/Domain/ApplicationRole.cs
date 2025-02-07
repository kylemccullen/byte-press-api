using Microsoft.AspNetCore.Identity;

namespace BytePress.Shared.Data.Domain;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole(string name): base(name)
    {
    }

    public List<ApplicationUserRole> UserRoles { get; set; }
}
