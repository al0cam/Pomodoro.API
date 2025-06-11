using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}
