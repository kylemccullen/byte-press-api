using Microsoft.AspNetCore.Identity;

namespace BytePress.Shared.Data.Domain.Identity;

public class ApplicationUserClaim : IdentityUserClaim<string>
{
    public ApplicationUser User { get; set; }
}
