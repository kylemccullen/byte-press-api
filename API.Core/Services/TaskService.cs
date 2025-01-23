using API.Core.DTO.Task;
using API.Core.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BytePress.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Services;

public class TaskService : ITaskService
{
    private readonly BytePressContext _context;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public TaskService(BytePressContext context, IUserService userService, IMapper mapper)
    {
        _context = context;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<List<TaskDto>> GetAsync()
    {
        return await _context.Task
            .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TaskDto> AddAsync(AddTaskDto taskDto)
    {
        var task = _mapper.Map<BytePress.Shared.Data.Domain.Task>(taskDto);

        task.User = await _userService.GetCurrentUserAsync();

        await _context.Task.AddAsync(task);
        await _context.SaveChangesAsync();

        return _mapper.Map<TaskDto>(task);
    }

    public async Task<TaskDto> UpdateAsync(int id, UpdateTaskDto taskDto)
    {
        var task = await _context.Task
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (!_userService.IsValidUser(task.UserId))
            throw new UnauthorizedAccessException();

        task.IsCompleted = taskDto.IsCompleted ?? task.IsCompleted;

        _context.Update(task);
        await _context.SaveChangesAsync();

        return _mapper.Map<TaskDto>(task);
    }
}
