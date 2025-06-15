using MediatR;

// Query: Request to get a specific task by ID for a user
public record GetTaskItemByIdQuery(int Id, string UserId) : IRequest<TaskItem?>;

// Handler: Logic to retrieve a single task by ID and verify ownership
public class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItem?>
{
    private readonly ITaskItemRepository _repository;

    public GetTaskItemByIdQueryHandler(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskItem?> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(request.Id);
        if (task != null && task.UserId != request.UserId)
        {
            return null; // Task found, but doesn't belong to this user
        }
        return task;
    }
}
