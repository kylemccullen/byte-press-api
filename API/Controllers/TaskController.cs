using API.Core.DTO.Task;
using API.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class TaskController : Controller
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BytePress.Shared.Data.Domain.Task>>> GetAsync()
    {
        var result = await _taskService.GetAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BytePress.Shared.Data.Domain.Task>> PostAsync([FromBody] AddTaskDto taskDto)
    {
        var result = await _taskService.AddAsync(taskDto);

        return Ok(result);
    }
}
