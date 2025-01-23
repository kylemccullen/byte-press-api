using BytePress.Shared.Data.Domain;

namespace API.Core.Interfaces;

public interface IUserService
{
    Task<ApplicationUser> GetCurrentUserAsync();
    bool IsValidUser(string entityUserId);
}
