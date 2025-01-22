using API.Core.DTO.Task;

namespace API.Core.Interfaces;

public interface ITaskService
{
    Task<List<BytePress.Shared.Data.Domain.Task>> GetAsync();
    Task<TaskDto> AddAsync(AddTaskDto taskDto);
}
