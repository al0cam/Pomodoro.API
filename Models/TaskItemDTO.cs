public class TaskItemDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public DateTime? DueAt { get; set; }
    public int? EstimatedPomodoros { get; set; }
    public int? CompletedPomodoros { get; set; }
}
