using MediatR;

// Query: Request to get all tasks for a specific user
public record GetUserTaskItemsQuery(string UserId) : IRequest<IEnumerable<TaskItem>>;

// Handler: Logic to retrieve tasks for a user
public class GetUserTaskItemsQueryHandler : IRequestHandler<GetUserTaskItemsQuery, IEnumerable<TaskItem>>
{
    private readonly ITaskItemRepository _repository;

    public GetUserTaskItemsQueryHandler(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItem>> Handle(GetUserTaskItemsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllByUserIdAsync(request.UserId);
    }
}
