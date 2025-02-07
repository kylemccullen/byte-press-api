using Microsoft.AspNetCore.Identity;

namespace BytePress.Shared.Data.Domain;

public class ApplicationUserClaim : IdentityUserClaim<string>
{
    public ApplicationUser User { get; set; }
}
