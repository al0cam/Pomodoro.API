using System.Security.Claims;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _repository;

    public TaskItemService(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(ClaimsPrincipal user)
    {
        var userId = GetUserId(user);
        return await _repository.GetAllByUserIdAsync(userId);
    }

    public async Task<TaskItem?> GetByIdAsync(int id, ClaimsPrincipal user)
    {
        var userId = GetUserId(user);

        var task = await _repository.GetByIdAsync(id);
        if (task != null && task.UserId != userId)
        {
            return null;
        }
        return task;
    }

    public async Task<TaskItem> AddAsync(TaskItemDTO taskDTO, ClaimsPrincipal user)
    {
        var userId = GetUserId(user);

        var task = new TaskItem
        {
            Title = taskDTO.Title,
            Description = taskDTO.Description,
            IsCompleted = taskDTO.IsCompleted ?? false,
            DueAt = taskDTO.DueAt,
            EstimatedPomodoros = taskDTO.EstimatedPomodoros,
            CompletedPomodoros = taskDTO.CompletedPomodoros ?? 0,
            CreatedAt = DateTime.UtcNow,
            UserId = userId

        };

        return await _repository.AddAsync(task);
    }

    public async Task<TaskItem> UpdateAsync(TaskItemDTO taskDTO, int id, ClaimsPrincipal user)
    {
        var userId = GetUserId(user);
        var existingTask = await _repository.GetByIdAsync(id);

        if (existingTask == null || existingTask.UserId != userId)
        {
            throw new KeyNotFoundException($"Task with id {id} not found or not authorized.");
        }

        existingTask.Title = taskDTO.Title ?? existingTask.Title;
        existingTask.Description = taskDTO.Description ?? existingTask.Description;
        existingTask.IsCompleted = taskDTO.IsCompleted ?? existingTask.IsCompleted;
        existingTask.DueAt = taskDTO.DueAt ?? existingTask.DueAt;
        existingTask.EstimatedPomodoros = taskDTO.EstimatedPomodoros ?? existingTask.EstimatedPomodoros;
        existingTask.CompletedPomodoros = taskDTO.CompletedPomodoros ?? existingTask.CompletedPomodoros;

        return await _repository.UpdateAsync(existingTask);
    }

    public async Task DeleteAsync(int id, ClaimsPrincipal user)
    {
        var userId = GetUserId(user);
        var existingTask = await _repository.GetByIdAsync(id);

        if (existingTask == null || existingTask.UserId != userId)
        {
            throw new KeyNotFoundException($"Task with id {id} not found or not authorized.");
        }

        await _repository.DeleteAsync(id);
    }

    private string GetUserId(ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException("User ID not found. User must be authenticated.");
        }
        return userId;
    }
}
