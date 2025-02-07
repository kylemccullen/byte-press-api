using Microsoft.AspNetCore.Identity;

namespace BytePress.Shared.Data.Domain.Identity;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public ApplicationUser User { get; set; }

    public ApplicationRole Role { get; set; }
}
