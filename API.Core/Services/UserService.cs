using API.Core.DTO.User;
using API.Core.Interfaces;
using AutoMapper;
using BytePress.Shared.Classes;
using BytePress.Shared.Data.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ApplicationUser> GetCurrentUserAsync()
    {
        var name = _httpContextAccessor.HttpContext?.User?.Identity.Name;

        if (name == null)
            return null;

        return await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == name);
    }

    public bool IsValidUser(string userId)
    {
        var loggedInUserId = _httpContextAccessor.HttpContext.User.GetUserId();

        return loggedInUserId != null && loggedInUserId == userId;
    }

    public async Task<BaseUserDto> UpdateAsync(string id, UpdateUserDto updateUserDto)
    {
        var user = await GetCurrentUserAsync();

        if (!IsValidUser(id))
            throw new UnauthorizedAccessException();

        if (string.IsNullOrEmpty(updateUserDto.Name))
            user.Name = null;
        else
            user.Name = updateUserDto.Name;

        await _userManager.UpdateAsync(user);

        return _mapper.Map<BaseUserDto>(user);
    }
}
