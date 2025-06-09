public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _repository;

    public TaskItemService(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<TaskItem> AddAsync(TaskItemDTO taskDTO)
    {
        var task = new TaskItem
        {
            Title = taskDTO.Title,
            Description = taskDTO.Description,
            IsCompleted = taskDTO.IsCompleted,
            DueAt = taskDTO.DueAt,
            EstimatedPomodoros = taskDTO.EstimatedPomodoros,
            CompletedPomodoros = taskDTO.CompletedPomodoros
        };

        return await _repository.AddAsync(task);
    }

    public async Task<TaskItem> UpdateAsync(TaskItemDTO taskDTO, int id)
    {
        var existingTask = await _repository.GetByIdAsync(id);
        if (existingTask == null)
        {
            throw new KeyNotFoundException($"Task with id {id} not found.");
        }

        // Update fields from DTO
        existingTask.Title = taskDTO.Title == null ? existingTask.Title : taskDTO.Title;
        existingTask.Description = taskDTO.Description == null ? existingTask.Description : taskDTO.Description;
        existingTask.IsCompleted = taskDTO.IsCompleted == null ? existingTask.IsCompleted : taskDTO.IsCompleted;
        existingTask.DueAt = taskDTO.DueAt == null ? existingTask.DueAt : taskDTO.DueAt;
        existingTask.EstimatedPomodoros = taskDTO.EstimatedPomodoros == null ? existingTask.EstimatedPomodoros : null;
        existingTask.CompletedPomodoros = taskDTO.CompletedPomodoros == null ? existingTask.CompletedPomodoros : null;

        return await _repository.UpdateAsync(existingTask);
    }

    public async Task DeleteAsync(int id)
    {
        var existingTask = await _repository.GetByIdAsync(id);
        if (existingTask == null)
        {
            throw new KeyNotFoundException($"Task with id {id} not found.");
        }

        await _repository.DeleteAsync(id);
    }
}

