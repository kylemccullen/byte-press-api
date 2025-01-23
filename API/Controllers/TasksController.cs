using API.Core.DTO.Task;
using API.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[Authorize]
public class TasksController : Controller
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskDto>>> GetAsync()
    {
        var result = await _taskService.GetAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> PostAsync([FromBody] AddTaskDto taskDto)
    {
        var result = await _taskService.AddAsync(taskDto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskDto>> UpdateAsync(int id, [FromBody] UpdateTaskDto taskDto)
    {
        var result = await _taskService.UpdateAsync(id, taskDto);

        return Ok(result);
    }

}
