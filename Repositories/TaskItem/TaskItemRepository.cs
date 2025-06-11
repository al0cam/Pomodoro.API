using Microsoft.EntityFrameworkCore;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly PomodorDbContext _context;

    public TaskItemRepository(PomodorDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(string userId)
    {
        return await _context.TaskItems
                             .Where(t => t.UserId == userId)
                             .ToListAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        // This method might not be needed anymore if all gets are user-specific,
        // or you might use it for admin roles. Consider its purpose.
        return await _context.TaskItems.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        // Note: The service layer (TaskItemService) is responsible for
        // checking if the task belongs to the user. The repository just fetches by ID.
        return await _context.TaskItems.FindAsync(id);
    }

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        _context.Entry(task).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task DeleteAsync(int id)
    {
        var taskToDelete = await _context.TaskItems.FindAsync(id);
        if (taskToDelete != null)
        {
            _context.TaskItems.Remove(taskToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
