using System.Security.Claims; // Required for ClaimsPrincipal

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _repository;

    public TaskItemService(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    // --- Retrieve all tasks for the current user ---
    public async Task<IEnumerable<TaskItem>> GetAllAsync(ClaimsPrincipal user)
    {
        var userId = GetUserId(user);
        return await _repository.GetAllByUserIdAsync(userId); // You'll need to add this method to your repository
    }

    // --- Retrieve a specific task for the current user ---
    public async Task<TaskItem?> GetByIdAsync(int id, ClaimsPrincipal user)
    {
        var userId = GetUserId(user);
        // Ensure the task belongs to the current user
        var task = await _repository.GetByIdAsync(id);
        if (task != null && task.UserId != userId)
        {
            return null; // Task found, but doesn't belong to this user
        }
        return task;
    }

    // --- Add a new task for the current user ---
    public async Task<TaskItem> AddAsync(TaskItemDTO taskDTO, ClaimsPrincipal user)
    {
        var userId = GetUserId(user);

        var task = new TaskItem
        {
            Title = taskDTO.Title,
            Description = taskDTO.Description,
            IsCompleted = taskDTO.IsCompleted ?? false, // Provide a default if nullable in DTO
            DueAt = taskDTO.DueAt,
            EstimatedPomodoros = taskDTO.EstimatedPomodoros,
            CompletedPomodoros = taskDTO.CompletedPomodoros ?? 0, // Provide a default if nullable in DTO
            CreatedAt = DateTime.UtcNow, // Set creation timestamp here
            UserId = userId // Assign the current user's ID
            // User navigation property will be set by EF Core during SaveChanges based on UserId
        };

        return await _repository.AddAsync(task);
    }

    // --- Update an existing task for the current user ---
    public async Task<TaskItem> UpdateAsync(TaskItemDTO taskDTO, int id, ClaimsPrincipal user)
    {
        var userId = GetUserId(user);
        var existingTask = await _repository.GetByIdAsync(id);

        if (existingTask == null || existingTask.UserId != userId) // Ensure task exists AND belongs to the user
        {
            throw new KeyNotFoundException($"Task with id {id} not found or not authorized.");
        }

        // Update fields from DTO
        // Use null-coalescing operator for nullable properties (??)
        existingTask.Title = taskDTO.Title ?? existingTask.Title;
        existingTask.Description = taskDTO.Description ?? existingTask.Description;
        existingTask.IsCompleted = taskDTO.IsCompleted ?? existingTask.IsCompleted;
        existingTask.DueAt = taskDTO.DueAt ?? existingTask.DueAt;
        existingTask.EstimatedPomodoros = taskDTO.EstimatedPomodoros ?? existingTask.EstimatedPomodoros;
        existingTask.CompletedPomodoros = taskDTO.CompletedPomodoros ?? existingTask.CompletedPomodoros;

        return await _repository.UpdateAsync(existingTask);
    }

    // --- Delete a task for the current user ---
    public async Task DeleteAsync(int id, ClaimsPrincipal user)
    {
        var userId = GetUserId(user);
        var existingTask = await _repository.GetByIdAsync(id);

        if (existingTask == null || existingTask.UserId != userId) // Ensure task exists AND belongs to the user
        {
            throw new KeyNotFoundException($"Task with id {id} not found or not authorized.");
        }

        await _repository.DeleteAsync(id);
    }

    // --- Helper method to extract UserId ---
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
