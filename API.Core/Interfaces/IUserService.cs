using API.Core.DTO.User;
using BytePress.Shared.Data.Domain;

namespace API.Core.Interfaces;

public interface IUserService
{
    Task<ApplicationUser> GetLoggedInUserAsync();
    Task<BaseUserDto> GetLoggedInUserWithRoleAsync();
    bool IsValidUser(string userId);
    Task<BaseUserDto> UpdateAsync(string id, UpdateUserDto updateUserDto);
    Task<List<UserOverviewDto>> GetOverviewsAsync();
}
