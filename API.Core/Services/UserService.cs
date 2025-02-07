using API.Core.DTO.User;
using API.Core.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BytePress.Shared.Classes;
using BytePress.Shared.Data;
using BytePress.Shared.Data.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly BytePressContext _context;
    private readonly IMapper _mapper;

    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IMapper mapper, BytePressContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _mapper = mapper;
        _context = context;
    }

    public async Task<ApplicationUser> GetLoggedInUserAsync()
    {
        var name = _httpContextAccessor.HttpContext?.User?.Identity.Name;

        if (name == null)
            return null;

        return await _userManager.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == name);
    }

    public async Task<BaseUserDto> GetLoggedInUserWithRoleAsync()
    {
        var email = _httpContextAccessor.HttpContext?.User?.Identity.Name;

        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ProjectTo<BaseUserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public bool IsValidUser(string userId)
    {
        var loggedInUserId = _httpContextAccessor.HttpContext.User.GetUserId();

        return loggedInUserId != null && loggedInUserId == userId;
    }

    public async Task<BaseUserDto> UpdateAsync(string id, UpdateUserDto updateUserDto)
    {
        var user = await GetLoggedInUserAsync();

        if (!IsValidUser(id))
            throw new UnauthorizedAccessException();

        if (string.IsNullOrEmpty(updateUserDto.Name))
            user.Name = null;
        else
            user.Name = updateUserDto.Name;

        await _userManager.UpdateAsync(user);

        return _mapper.Map<BaseUserDto>(user);
    }

    public async Task<List<UserOverviewDto>> GetOverviewsAsync()
    {
        return await _context.Users
            .Include(u => u.Tasks)
            .ProjectTo<UserOverviewDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
