using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TaskItemController : ControllerBase
{
    private readonly ITaskItemRepository _repository;

    public TaskItemController(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var taskItems = _repository.GetAllAsync().Result;
        if (taskItems == null || !taskItems.Any())
        {
            return NotFound();
        }
        return Ok(taskItems);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var taskItem = _repository.GetByIdAsync(id).Result;
        if (taskItem == null)
        {
            return NotFound();
        }
        return Ok(taskItem);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TaskItem taskItem)
    {
        if (taskItem == null)
        {
            return BadRequest("Task item cannot be null.");
        }

        _repository.AddAsync(taskItem).Wait();
        return CreatedAtAction(nameof(GetById), new { id = taskItem.Id }, taskItem);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TaskItem taskItem)
    {
        if (taskItem == null || taskItem.Id != id)
        {
            return BadRequest("Task item is invalid.");
        }

        var existingTaskItem = _repository.GetByIdAsync(id).Result;
        if (existingTaskItem == null)
        {
            return NotFound();
        }

        _repository.UpdateAsync(taskItem).Wait();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existingTaskItem = _repository.GetByIdAsync(id).Result;
        if (existingTaskItem == null)
        {
            return NotFound();
        }

        _repository.DeleteAsync(id).Wait();
        return NoContent();
    }
}
