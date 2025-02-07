using API.Core.DTO.User;
using API.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[Authorize]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<BaseUserDto>> GetAsync()
    {
        var result = await _userService.GetLoggedInUserWithRoleAsync();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseUserDto>> UpdateAsync(string id, [FromBody] UpdateUserDto updateUserDto)
    {
        var result = await _userService.UpdateAsync(id, updateUserDto);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("overview")]
    public async Task<ActionResult<List<UserOverviewDto>>> GetOverviewsAsync()
    {
        var result = await _userService.GetOverviewsAsync();

        return Ok(result);
    }
}
