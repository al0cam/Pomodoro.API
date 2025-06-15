using MediatR;

public record UpdateTaskItemCommand(
    int Id,
    string? Title,
    string? Description,
    bool? IsCompleted,
    DateTime? DueAt,
    int? EstimatedPomodoros,
    int? CompletedPomodoros,
    string UserId
) : IRequest<TaskItem>;


public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand, TaskItem>
{
    private readonly ITaskItemRepository _repository;

    public UpdateTaskItemCommandHandler(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskItem> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
    {

        var existingTask = await _repository.GetByIdAsync(request.Id);


        if (existingTask == null || existingTask.UserId != request.UserId)
        {

            throw new KeyNotFoundException($"Task with id {request.Id} not found or not authorized.");
        }


        existingTask.Title = request.Title ?? existingTask.Title;
        existingTask.Description = request.Description ?? existingTask.Description;
        existingTask.IsCompleted = request.IsCompleted ?? existingTask.IsCompleted;
        existingTask.DueAt = request.DueAt ?? existingTask.DueAt;
        existingTask.EstimatedPomodoros = request.EstimatedPomodoros ?? existingTask.EstimatedPomodoros;
        existingTask.CompletedPomodoros = request.CompletedPomodoros ?? existingTask.CompletedPomodoros;


        return await _repository.UpdateAsync(existingTask);
    }
}
