using Microsoft.EntityFrameworkCore;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly TaskItemContext _context;

    public TaskItemRepository(TaskItemContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.TaskItems.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.TaskItems.FindAsync(id);
    }

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        await _context.TaskItems.AddAsync(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        _context.TaskItems.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task DeleteAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task != null)
        {
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
