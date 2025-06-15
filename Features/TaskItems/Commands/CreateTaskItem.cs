using MediatR;

public record CreateTaskItemCommand(
    string Title,
    string? Description,
    bool? IsCompleted,
    DateTime? DueAt,
    int? EstimatedPomodoros,
    int? CompletedPomodoros,
    string UserId
) : IRequest<TaskItem>;

public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, TaskItem>
{
    private readonly ITaskItemRepository _repository;

    public CreateTaskItemCommandHandler(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskItem> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = request.IsCompleted ?? false,
            DueAt = request.DueAt,
            EstimatedPomodoros = request.EstimatedPomodoros,
            CompletedPomodoros = request.CompletedPomodoros ?? 0,
            CreatedAt = DateTime.UtcNow,
            UserId = request.UserId
        };

        return await _repository.AddAsync(task);
    }
}
