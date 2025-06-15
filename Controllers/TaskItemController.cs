using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskItemController : ControllerBase
{
    private readonly IMediator _mediator;



    public TaskItemController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllTasks()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in claims.");
        }

        var query = new GetUserTaskItemsQuery(userId);
        var tasks = await _mediator.Send(query);

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTaskById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in claims.");
        }

        var query = new GetTaskItemByIdQuery(id, userId);
        var task = await _mediator.Send(query);

        if (task == null)
        {
            return NotFound($"Task with ID {id} not found or you don't have access.");
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> AddTask([FromBody] TaskItemDTO taskDTO)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in claims.");
        }

        var command = new CreateTaskItemCommand(
            taskDTO.Title!,
            taskDTO.Description,
            taskDTO.IsCompleted,
            taskDTO.DueAt,
            taskDTO.EstimatedPomodoros,
            taskDTO.CompletedPomodoros,
            userId
        );

        var addedTask = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTaskById), new { id = addedTask.Id }, addedTask);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskItem>> UpdateTask(int id, [FromBody] TaskItemDTO taskDTO)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in claims.");
        }

        var command = new UpdateTaskItemCommand(
            id,
            taskDTO.Title,
            taskDTO.Description,
            taskDTO.IsCompleted,
            taskDTO.DueAt,
            taskDTO.EstimatedPomodoros,
            taskDTO.CompletedPomodoros,
            userId
        );

        try
        {
            var updatedTask = await _mediator.Send(command);
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in claims.");
        }

        var command = new DeleteTaskItemCommand(id, userId);

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
