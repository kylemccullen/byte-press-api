using API.Core.DTO.Task;

namespace API.Core.Interfaces;

public interface ITaskService
{
    Task<List<TaskDto>> GetAsync();
    Task<TaskDto> AddAsync(AddTaskDto taskDto);
    Task<TaskDto> UpdateAsync(int id, UpdateTaskDto taskDto);
}
