using API.Core.Interfaces;
using BytePress.Shared.Data.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Core.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<ApplicationUser> GetCurrentUserAsync()
    {
        var name = _httpContextAccessor.HttpContext?.User?.Identity.Name;

        if (name == null)
            return null;

        return await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == name);
    }

    public bool IsValidUser(string entityUserId)
    {
        var loggedInUserId = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        return loggedInUserId != null && loggedInUserId == entityUserId;
    }
}
