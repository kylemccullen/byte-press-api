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
        var user = await _userService.GetCurrentUserAsync();

        var result = _mapper.Map<BaseUserDto>(user);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseUserDto>> UpdateAsync(string id, [FromBody] UpdateUserDto updateUserDto)
    {
        var result = await _userService.UpdateAsync(id, updateUserDto);

        return Ok(result);
    }
}
