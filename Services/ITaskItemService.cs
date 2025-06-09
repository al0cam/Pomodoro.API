
public interface ITaskItemService
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> AddAsync(TaskItemDTO taskDTO);
    Task<TaskItem> UpdateAsync(TaskItemDTO taskDTO, int id);
    Task DeleteAsync(int id);
}
