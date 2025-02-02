using API.Core.DTO.User;
using BytePress.Shared.Data.Domain;

namespace API.Core.Interfaces;

public interface IUserService
{
    Task<ApplicationUser> GetCurrentUserAsync();
    bool IsValidUser(string userId);
    Task<BaseUserDto> UpdateAsync(string id, UpdateUserDto updateUserDto);
}
