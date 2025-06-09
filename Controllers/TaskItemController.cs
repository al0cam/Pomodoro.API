using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TaskItemController : ControllerBase
{
    private readonly ITaskItemService _service;

    public TaskItemController(ITaskItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var taskItems = await _service.GetAllAsync();
        if (taskItems == null || !taskItems.Any())
        {
            return NotFound();
        }
        return Ok(taskItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var taskItem = await _service.GetByIdAsync(id);
        if (taskItem == null)
        {
            return NotFound();
        }
        return Ok(taskItem);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskItemDTO taskDTO)
    {
        if (taskDTO == null)
            return BadRequest("Task item cannot be null.");

        var createdTask = await _service.AddAsync(taskDTO);
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskItemDTO taskDTO)
    {
        if (taskDTO == null)
        {
            return BadRequest("Task item is invalid.");
        }

        try
        {
            await _service.UpdateAsync(taskDTO, id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
