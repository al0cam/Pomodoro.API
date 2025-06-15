using MediatR;

public record DeleteTaskItemCommand(
    int Id,
    string UserId
) : IRequest<Unit>;


public class DeleteTaskItemCommandHandler : IRequestHandler<DeleteTaskItemCommand, Unit>
{
    private readonly ITaskItemRepository _repository;

    public DeleteTaskItemCommandHandler(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
    {

        var existingTask = await _repository.GetByIdAsync(request.Id);


        if (existingTask == null || existingTask.UserId != request.UserId)
        {

            throw new KeyNotFoundException($"Task with id {request.Id} not found or not authorized.");
        }


        await _repository.DeleteAsync(request.Id);


        return Unit.Value;
    }
}
