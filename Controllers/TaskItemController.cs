using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Ensures only authenticated users can access these endpoints
public class TaskItemsController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemsController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllTasks()
    {
        // Pass the current user to the service
        var tasks = await _taskItemService.GetAllAsync(User);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTaskById(int id)
    {
        // Pass the current user to the service
        var task = await _taskItemService.GetByIdAsync(id, User);
        if (task == null)
        {
            // This could mean not found or not authorized
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> AddTask(TaskItemDTO taskDTO)
    {
        // Pass the current user to the service
        var addedTask = await _taskItemService.AddAsync(taskDTO, User);
        return CreatedAtAction(nameof(GetTaskById), new { id = addedTask.Id }, addedTask);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskItem>> UpdateTask(int id, TaskItemDTO taskDTO)
    {
        try
        {
            // Pass the current user to the service
            var updatedTask = await _taskItemService.UpdateAsync(taskDTO, id, User);
            return Ok(updatedTask);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            // Pass the current user to the service
            await _taskItemService.DeleteAsync(id, User);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
