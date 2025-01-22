using API.Core.DTO.Task;
using API.Core.Interfaces;
using AutoMapper;
using BytePress.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Services;

public class TaskService : ITaskService
{
    private readonly BytePressContext _context;
    private readonly IMapper _mapper;

    public TaskService(BytePressContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BytePress.Shared.Data.Domain.Task>> GetAsync()
    {
        return await _context.Task
            .ToListAsync();
    }

    public async Task<TaskDto> AddAsync(AddTaskDto taskDto)
    {
        var task = _mapper.Map<BytePress.Shared.Data.Domain.Task>(taskDto);

        await _context.Task.AddAsync(task);
        await _context.SaveChangesAsync();

        return _mapper.Map<TaskDto>(task);
    }
}
