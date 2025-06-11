using System.Security.Claims;

public interface ITaskItemService
{
    Task<TaskItem?> GetByIdAsync(int id, ClaimsPrincipal user);
    Task<IEnumerable<TaskItem>> GetAllAsync(ClaimsPrincipal user);
    Task<TaskItem> AddAsync(TaskItemDTO taskDTO, ClaimsPrincipal user);
    Task<TaskItem> UpdateAsync(TaskItemDTO taskDTO, int id, ClaimsPrincipal user);
    Task DeleteAsync(int id, ClaimsPrincipal user);
}
